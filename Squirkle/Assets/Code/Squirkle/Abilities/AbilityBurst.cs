using UnityEngine;

namespace Squirkle
{
    public class AbilityBurst : Ability
    {
        public override string GetMetaID() => "ABILITY_BURST";
        public override bool IsActive() => false;

        public override void Register() => AbilityEvents.onEnemyKilled += Perform;
        public override void UnRegister() => AbilityEvents.onEnemyKilled -= Perform;

        public void Perform(EnemyInstance enemy)
        {
            AttackStats playerAttack = PlayerData.weaponData.stats;

            DamageSource aoe = new DamageSource(enemy.position, playerAttack.Multiply(0.1f), 30f, 1.5f);
            EnemySpawner.inst.queuedDamageSources.Add(aoe);
        }
    }
}
