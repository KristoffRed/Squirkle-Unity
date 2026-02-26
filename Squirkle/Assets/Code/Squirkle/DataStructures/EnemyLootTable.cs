using Redline.Helpers;
using UnityEngine;

namespace Squirkle
{
    [CreateAssetMenu(fileName = "EnemyLoot", menuName = "Squirkle/Enemy Loot", order = 0)]
    public class EnemyLootTable : LootTable<EnemyLoot>
    {
        
    }

    [System.Serializable]
    public class EnemyLoot
    {
        public int coins = 0;
        public string itemID = "";

        public void Add()
        {   
            #if UNITY_WEBGL && !UNITY_EDITOR
            if (coins > 0) JSBridge.OnPlayerGiveCoins(coins);
            if (itemID != "") JSBridge.OnPlayerAddItem(itemID);
            #endif
        }
    }
}
