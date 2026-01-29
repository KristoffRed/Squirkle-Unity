using Redline.Helpers;
using Redline.Resources;
using UnityEngine;

namespace Squirkle
{
    public class EnemyInstance
    {
        public float collisionRadius = 0.5f;
        public GameObject gameObject;
        public Transform transform => gameObject.transform;

        private EnemySpawner spawner;
        private Vector2 direction;

        public EnemyInstance(GameObjectPool pool, EnemySpawner _spawner)
        {
            direction = ((Vector2)GlobalHelper.RandomDirection()).normalized;
            spawner = _spawner;
            gameObject = pool.Get();
            transform.position = spawner.GetRandomPositionOnScreen();
        }

        public void Update(Vector2 slashPosition, bool isSlashing)
        {
            MoveAround();

            if (isSlashing)
            {
                float distance = Vector2.Distance(slashPosition, transform.position);
                
                if (distance < collisionRadius)
                {
                    Damage();
                }
            }
        }

        private void MoveAround()
        {
            transform.position += (Vector3)(direction * 0.25f * Time.deltaTime);

            if (spawner.IsOutOfBounds(transform.position))
            {
                direction *= -1f;
            }
        }

        private void Damage()
        {
            spawner.Kill(this);
        }
    }
}
