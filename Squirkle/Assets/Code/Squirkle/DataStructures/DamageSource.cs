using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class DamageSource
    {
        public Vector2 pos;
        public AttackStats attackStats;
        public float knockback;
        
        public DamageSource(Vector2 _pos, AttackStats _attack, float _knockback)
        {
            pos = _pos;
            attackStats = _attack;
            knockback = _knockback;
        }
    }
}
