using DG.Tweening;
using Redline.Helpers;
using Redline.Resources;
using UnityEngine;

namespace Squirkle
{
    public class EnemyInstance
    {
        public float health = 20f;
        public float maxSpeed = 0.5f;
        public float collisionRadius = 0.5f;
        public GameObject gameObject;
        public Transform transform => gameObject.transform;
        public SpriteRenderer spriteRenderer;

        private Vector2 position => (Vector2)transform.position;
        private EnemySpawner spawner;
        private Vector2 targetDirection;
        private Vector2 velocity;

        public EnemyInstance(GameObjectPool pool, EnemySpawner _spawner)
        {
            health = 20f;

            targetDirection = ((Vector2)GlobalHelper.RandomDirection()).normalized;
            spawner = _spawner;
            gameObject = pool.Get();
            spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            transform.position = spawner.GetRandomPositionOnScreen();
        }

        public void Update(WeaponData weapon, DamageSource slashDamage, Vector2 slashPosition, bool isSlashing)
        {
            MoveAround();

            if (isSlashing)
            {
                float distance = Vector2.Distance(slashPosition, transform.position);
                
                if (distance < collisionRadius)
                {
                    Damage(slashDamage);
                }
            }
        }

        private void MoveAround()
        {
            velocity += targetDirection * 0.1f;
            velocity = Vector2.Lerp(velocity, velocity.LimitLength(maxSpeed), Time.deltaTime * 10f);
            transform.position += (Vector3)(velocity * Time.deltaTime);

            if (spawner.IsOutOfBounds(position))
            {
                velocity *= -1f;
                targetDirection *= -1f;
            }
        }

        private void Damage(DamageSource source)
        {
            Vector2 knockbackDirection = (position - source.pos).normalized;
            velocity += knockbackDirection * source.knockback;
            
            health -= source.damage;

            OnHitFX();
            CheckDeath();
        }

        private void OnHitFX()
        {
            Color baseColor = new Color32(255, 86, 66, 255);
            spriteRenderer.color = Color.white;
            spriteRenderer.DOColor(baseColor, 0.3f).SetDelay(0.1f);
        }

        private void CheckDeath()
        {
            if (health > 0f) return;

            spawner.Kill(this);
        }
    }
}
