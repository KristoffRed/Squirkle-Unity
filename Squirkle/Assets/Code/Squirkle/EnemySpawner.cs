using System.Collections.Generic;
using DG.Tweening;
using Redline.Helpers;
using Redline.Resources;
using UnityEngine;

namespace Squirkle
{
    public class EnemySpawner : Singleton<EnemySpawner>
    {
        public float spawnMargin = 50;
        public CursorSlash slasher;
        public GameObjectPool enemyPool;
        public VFXPool deathVFXPool;
        public HitmarkerManager hitmarkers;
        public float spawnCooldown = 0.1f;
        public List<EnemyData> enemyDatas = new List<EnemyData>();
        public List<EnemyInstance> enemies = new List<EnemyInstance>();
        public List<DamageSource> queuedDamageSources = new List<DamageSource>();


        private float timer = 0f;

        void Update()
        {
            timer += Time.deltaTime;

            while (timer > spawnCooldown)
            {
                timer -= spawnCooldown;
                SpawnEnemy();
            }

            UpdateEnemies();
        }

        private void UpdateEnemies()
        {
            bool isSlashing = slasher.IsSlashing();
            Vector3 slashPosition = slasher.transform.position;
            DamageSource slashDamage = isSlashing ? PlayerData.weaponData.GetDamage(slashPosition) : null;

            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyInstance enemy = enemies[i];

                enemy.Update(slashDamage, queuedDamageSources);
            }

            // Clear damage sources
            queuedDamageSources.Clear();

            List<EnemyInstance> newEnemies = new List<EnemyInstance>();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].IsDead() && !enemies[i].handledDeath)
                {
                    Kill(enemies[i]);
                }
                else
                {
                    newEnemies.Add(enemies[i]);
                }
            }  

            enemies = newEnemies;
        }

        private void SpawnEnemy()
        {
            enemies.Add(new EnemyInstance(enemyDatas.PickRandom(), enemyPool, this));
        }

        public Vector2 ScreenCornerMin() => Camera.main.ScreenToWorldPoint(new Vector2(spawnMargin, spawnMargin));
        public Vector2 ScreenCornerMax() => Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - spawnMargin, Screen.height - spawnMargin));

        public bool IsOutOfBounds(Vector2 pos)
        {
            Vector2 min = ScreenCornerMin();
            Vector2 max = ScreenCornerMax();

            return pos.x < min.x || pos.y < min.y || pos.x > max.x || pos.y > max.y;
        }
        
        public void ClampToBounds(Transform t)
        {
            Vector2 min = ScreenCornerMin();
            Vector2 max = ScreenCornerMax();

            t.position = new Vector2(
                Mathf.Clamp(t.position.x, min.x, max.x),
                Mathf.Clamp(t.position.y, min.y, max.y)
            );
        }

        public Vector2 GetRandomPositionOnScreen()
        {
            Vector2 min = ScreenCornerMin();
            Vector2 max = ScreenCornerMax();

            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }

        public void Kill(EnemyInstance enemy)
        {
            if (enemy.handledDeath) return;

            #if UNITY_WEBGL && !UNITY_EDITOR
            JSBridge.OnPlayerGiveCoins(1);
            #endif

            AbilityEvents.onEnemyKilled?.Invoke(enemy);
            enemy.handledDeath = true;
            SpawnDeathVFX(enemy.position, enemy.velocity, enemy.enemyData.color2);

            enemy.CleanUpFX();
            enemyPool.Release(enemy.gameObject);
        }

        public void SpawnDeathVFX(Vector2 position, Vector2 velocity, Color color)
        {
            ParticleGroup vfx = deathVFXPool.Get();
            vfx.transform.position = position;
            
            // rotation
            Vector2 direction = velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		    vfx.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            // speed
            var fx = vfx.GetParticle(0).main;
            fx.startSpeedMultiplier = velocity.magnitude / 2f;

            // color
            var col = vfx.GetParticle(0).colorOverLifetime;
            var minMaxGradient = col.color;
            var gradient = minMaxGradient.gradient;
            
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(color, 0.3f) },
                gradient.alphaKeys
            );

            minMaxGradient.gradient = gradient;
            col.color = gradient;
        }
    }
}
