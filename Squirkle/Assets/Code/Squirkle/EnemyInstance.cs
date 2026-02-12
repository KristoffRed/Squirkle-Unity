using System;
using DG.Tweening;
using Redline.Helpers;
using Redline.Resources;
using UnityEngine;

namespace Squirkle
{
    public class EnemyInstance
    {
        public static Color baseColor = new Color32(255, 86, 66, 255);

        public float health = 20f;
        public ResistanceData resistances = new ResistanceData();
        public float maxSpeed = 0.5f;
        public float collisionRadius = 0.5f;
        public GameObject gameObject;
        public Transform transform => gameObject.transform;
        public SpriteRenderer spriteRenderer;

        public Vector2 position => (Vector2)transform.position;
        private EnemySpawner spawner;
        private Vector2 targetDirection = Vector2.zero;
        public Vector2 velocity = Vector2.zero;
        public bool handledDeath = false;

        public EnemyInstance(GameObjectPool pool, EnemySpawner _spawner)
        {
            health = 20f;
            handledDeath = false;

            targetDirection = ((Vector2)GlobalHelper.RandomDirection()).normalized;
            spawner = _spawner;
            gameObject = pool.Get();
            spriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            transform.position = spawner.GetRandomPositionOnScreen();

            // fx stuff
            spriteRenderer.color = baseColor;
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
            // Apply knockback
            Vector2 knockbackDirection = (position - source.pos).normalized;
            velocity += knockbackDirection * source.knockback;
            
            // Create damage data structure
            DamageValue damage = new DamageValue(source.attackStats);

            // Apply resistances
            damage = resistances.ApplyToDamage(damage);
            
            // Apply critical strike
            damage = damage.ApplyCriticalStrike(source.attackStats.critChance, source.attackStats.circleDamage);

            // Deal damage
            health -= damage.GetTotalDamage();

            OnHitFX();
        }

        private void OnHitFX()
        {
            spriteRenderer.color = Color.white;
            spriteRenderer.DOColor(baseColor, 0.3f).SetDelay(0.1f);
        }

        public bool IsDead()
        {
            return health <= 0f;
        }

        public bool IsNull()
        {
            return transform == null;
        }

        public void CleanUpFX()
        {
            spriteRenderer.DOKill();
        }
    }
}
