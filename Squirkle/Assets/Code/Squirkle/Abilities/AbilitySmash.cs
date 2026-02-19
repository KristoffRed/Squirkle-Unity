using UnityEngine;

namespace Squirkle
{
    public class AbilitySmash : Ability
    {
        public override string GetMetaID() => "ABILITY_SMASH";
        public override bool IsActive() => true;

        public override void Register() => AbilityEvents.onAbilityActivated += Perform;
        public override void UnRegister() => AbilityEvents.onAbilityActivated -= Perform;

        public void Perform(Vector2 pos)
        {
            AttackStats playerAttack = PlayerData.weaponData.stats;
            DamageSource aoe = new DamageSource(pos, playerAttack.Multiply(3f), 40f, 2f);

            EnemySpawner.inst.queuedDamageSources.Add(aoe);
        }
    }
}
