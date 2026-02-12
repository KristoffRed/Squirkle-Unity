using UnityEngine;

namespace Squirkle
{
    public struct DamageValue
    {
        public float circleDamage;
        public float squareDamage;
        public float triangleDamage;
        public bool isCrit;

        public DamageValue(AttackStats stats)
        {
            circleDamage = stats.circleDamage;
            squareDamage = stats.squareDamage;
            triangleDamage = stats.triangleDamage;
            isCrit = false;
        }

        public float GetTotalDamage() => circleDamage + squareDamage + triangleDamage;
        public DamageValue ApplyCriticalStrike(float critChance, float critDamage)
        {
            float rng = Random.Range(0f, 100f);
            if (rng > critChance) return this;

            isCrit = true;
            circleDamage *= critDamage;
            squareDamage *= critDamage;
            triangleDamage *= critDamage;

            return this;
        }
    }
}
