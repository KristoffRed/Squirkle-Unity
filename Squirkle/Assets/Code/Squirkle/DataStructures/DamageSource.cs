using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class DamageSource
    {
        public Vector2 pos;
        public AttackStats attackStats;
        public float knockback;
        public float radius;
        public bool ignoreIFrames;

        public DamageSource(Vector2 _pos, AttackStats _attack, float _knockback, float _radius, bool _ignoreIFrames = false)
        {
            pos = _pos;
            attackStats = _attack;
            knockback = _knockback;
            radius = _radius;
            ignoreIFrames = _ignoreIFrames;
        }
    }
}
