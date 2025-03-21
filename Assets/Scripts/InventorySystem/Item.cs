using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string itemName;
        public int cost;
        public int exp; 
        public int profitPrice; 
        public GameObject itemPrefab;
    }
}
