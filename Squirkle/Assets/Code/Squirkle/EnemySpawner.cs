using System.Collections.Generic;
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

            for (int i = 0; i < enemies.Count; i++)
            {
                EnemyInstance enemy = enemies[i];

                enemy.Update(slasher.transform.position, isSlashing);
            }
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
            enemyPool.Release(enemy.gameObject);
            enemies.Remove(enemy);
        }
    }
}
