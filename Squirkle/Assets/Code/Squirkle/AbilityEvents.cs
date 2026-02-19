using System;
using UnityEngine;

namespace Squirkle
{
    public static class AbilityEvents
    {
        // Actions
        public static Action<EnemyInstance> onEnemyKilled;
        public static Action<EnemyInstance, DamageValue> onEnemyHit;
    }
}
