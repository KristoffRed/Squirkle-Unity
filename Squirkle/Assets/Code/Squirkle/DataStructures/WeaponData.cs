using System.Collections.Generic;
using UnityEngine;

namespace Squirkle
{
    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public string description;
        public float knockback;
        public AttackStats stats;
        
        public DamageSource GetDamage(Vector2 pos)
        {
            return new DamageSource(pos, stats, knockback, 0f);
        }

        public List<Ability> GetAbilities()
        {
            List<Ability> result = new List<Ability>();

            foreach (string metaID in stats.metadata)
            {
                WeaponMetadata meta = MetadataGetter.GetWeaponMeta(metaID);

                if (meta.IsAbility()) result.Add(meta as Ability);
            }

            return result;
        }

        public void RegisterAbilities() => GetAbilities().ForEach(x => x.Register());
        public void UnRegisterAbilities() => GetAbilities().ForEach(x => x.UnRegister());
    }
}
