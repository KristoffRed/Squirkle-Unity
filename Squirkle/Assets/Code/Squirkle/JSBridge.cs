using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Squirkle
{
    public class JSBridge : MonoBehaviour
    {
        public static float baseTime = 0f;
    
        public void SetPlayerWeapon(string weaponJSON)
        {
            try
            {
                WeaponData data = (WeaponData)JsonUtility.FromJson(weaponJSON, typeof(WeaponData));
                PlayerData.weaponData = data;

                Debug.Log("Successfully set player weapon!");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error with JSON conversion at SetPlayerWeapon():\n{ex}");
            }
        }

        public void LoadArea(int areaID) => AreaManager.inst.LoadArea(areaID);
        public void SetGameTime(float timeInSeconds) => baseTime = timeInSeconds;

        [DllImport("__Internal")]
        public static extern void OnPlayerGiveCoins(int coins);
    }
}
