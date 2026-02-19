using UnityEngine;

namespace Squirkle
{
    public abstract class Ability : WeaponMetadata
    {
        public override bool IsAbility() => true;
        public abstract bool IsActive();
        public abstract void Register();
        public abstract void UnRegister();
    }
}
