using System.Collections.Generic;
using UnityEngine;

namespace Squirkle
{
    public static class MetadataGetter
    {
        private static Dictionary<string, WeaponMetadata> weaponMetadata;

        private static WeaponMetadata[] weaponMetadataInstances = new WeaponMetadata[]
        {
            new AbilityBurst(),
            new AbilitySmash()
        };

        static MetadataGetter()
        {
            weaponMetadata = new Dictionary<string, WeaponMetadata>();

            foreach (WeaponMetadata meta in weaponMetadataInstances)
            {
                weaponMetadata.Add(meta.GetMetaID(), meta);
            }
        }

        public static WeaponMetadata GetWeaponMeta(string metaID) => weaponMetadata[metaID];
    }
}
