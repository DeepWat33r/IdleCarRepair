using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public List<InventoryItem> inventoryItems;
        
        public static event Action OnInventoryChanged;

        public void Awake()
        {
            GameManager.Instance.InventoryManager = this;
        }

        public bool BuyItem(Item item)
        {
            if (GameManager.Instance.PlayerProgression.Money >= item.cost)
            {
                GameManager.Instance.PlayerProgression.SpendMoney(item.cost);

                InventoryItem invItem = inventoryItems.Find(i => i.item == item);
                if (invItem != null)
                {
                    invItem.quantity++;
                }
                else
                {
                    inventoryItems.Add(new InventoryItem { item = item, quantity = 1 });
                }

                OnInventoryChanged?.Invoke();
                SaveSystem.SaveSystem.Save();
                return true;
            }
            else
            {
                //Debug.Log("Not enough money!");
                return false;
            }
        }

        public void UseItem(Item item)
        {
            Debug.Log("Using item: " + item.name);
            InventoryItem invItem = inventoryItems.Find(i => i.item == item);
            if (invItem != null && invItem.quantity > 0)
            {
                invItem.quantity--;
                GameManager.Instance.PlayerProgression.AddMoney(item.profitPrice);
                GameManager.Instance.PlayerProgression.AddExperience(item.exp);

                OnInventoryChanged?.Invoke();
            }
        }
        #region Save/Load
        public void Save(ref InventoryData data)
        {
            data.inventoryItems = inventoryItems;
            OnInventoryChanged?.Invoke();
        }
        public void Load(InventoryData data)
        {
            inventoryItems = data.inventoryItems; 
            OnInventoryChanged?.Invoke();
        }
        #endregion
    }
    [Serializable]
    public struct InventoryData
    {
        public List<InventoryItem> inventoryItems;
    }
}
