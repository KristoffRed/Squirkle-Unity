using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public string description;
        public AttackStats stats;
        public float knockback;
        
        public DamageSource GetDamage(Vector2 pos)
        {
            return new DamageSource(pos, stats, knockback);
        }
    }
}
