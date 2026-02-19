using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class AttackStats
    {
        public float circleDamage;
        public float squareDamage;
        public float triangleDamage;

        public float critChance;
        public float critDamage;
        public string[] metadata;

        public AttackStats Clone() => (AttackStats)MemberwiseClone();

        public AttackStats Multiply(float value)
        {
            AttackStats result = Clone();

            result.circleDamage *= value;
            result.squareDamage *= value;
            result.triangleDamage *= value;

            return result;
        }
    }
}
