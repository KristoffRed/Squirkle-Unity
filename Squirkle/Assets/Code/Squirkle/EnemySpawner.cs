using System.Collections.Generic;
using DG.Tweening;
using Redline.Helpers;
using Redline.Resources;
using UnityEngine;

namespace Squirkle
{
    public class EnemySpawner : MonoBehaviour
    {
        public float spawnMargin = 50;
        public CursorSlash slasher;
        public GameObjectPool enemyPool;
        public VFXPool deathVFXPool;
        public float spawnCooldown = 0.1f;
        public List<EnemyInstance> enemies = new List<EnemyInstance>();

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
            DamageSource slashDamage = slasher.weaponData.GetDamage(slashPosition);

            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyInstance enemy = enemies[i];

                enemy.Update(slasher.weaponData, slashDamage, slashPosition, isSlashing);
            }

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
            enemies.Add(new EnemyInstance(enemyPool, this));
        }

        public Vector2 ScreenCornerMin() => Camera.main.ScreenToWorldPoint(new Vector2(spawnMargin, spawnMargin));
        public Vector2 ScreenCornerMax() => Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - spawnMargin, Screen.height - spawnMargin));

        public bool IsOutOfBounds(Vector2 pos)
        {
            Vector2 min = ScreenCornerMin();
            Vector2 max = ScreenCornerMax();

            return pos.x < min.x || pos.y < min.y || pos.x > max.x || pos.y > max.y;
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

            enemy.handledDeath = true;
            SpawnDeathVFX(enemy.position, enemy.velocity);

            enemy.CleanUpFX();
            enemyPool.Release(enemy.gameObject);
        }

        public void SpawnDeathVFX(Vector2 position, Vector2 velocity)
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
        }
    }
}
