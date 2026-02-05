using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class WeaponData
    {
        public float damage;
        public float knockback;
        
        public DamageSource GetDamage(Vector2 pos)
        {
            return new DamageSource(pos, damage, knockback);
        }
    }
}
