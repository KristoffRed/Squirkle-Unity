using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class DamageSource
    {
        public Vector2 pos;
        public float damage;
        public float knockback;
        
        public DamageSource(Vector2 _pos, float _damage, float _knockback)
        {
            pos = _pos;
            damage = _damage;
            knockback = _knockback;
        }
    }
}
