using UnityEngine;

namespace Squirkle
{
    public abstract class WeaponMetadata
    {
        public abstract string GetMetaID();
        public virtual bool IsAbility() => false;
    }
}
