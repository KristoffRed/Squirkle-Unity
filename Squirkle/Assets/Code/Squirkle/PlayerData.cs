using UnityEngine;

namespace Squirkle
{
    public class PlayerData : MonoBehaviour
    {
        public static WeaponData weaponData;
        public WeaponData defaultWeapon;

        void Start()
        {
            EquipWeapon(defaultWeapon);
        }

        public static void EquipWeapon(WeaponData weapon)
        {
            weaponData?.UnRegisterAbilities();
            weaponData = weapon;
            weaponData?.RegisterAbilities();
        }
    }
}
