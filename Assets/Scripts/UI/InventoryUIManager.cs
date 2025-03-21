using System;
using Core;
using InventorySystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InventoryUIManager : MonoBehaviour
    {
        public Item item;
        
        public TMP_Text quantityText;
        public TMP_Text priceText;

        private InventoryManager _inventoryManager;
        public void Awake()
        {
            _inventoryManager = GameManager.Instance.InventoryManager;
            InventoryManager.OnInventoryChanged += UpdateUI;
        }

        public void Start()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            
            InventoryItem invItem = _inventoryManager.inventoryItems.Find(i => i.item == item);

            if (quantityText != null)
            {
                quantityText.text = invItem.quantity.ToString();
            }
            if (priceText != null)
            {
                priceText.text = $"\u20ac" + invItem.item.cost;
            }
        }

        public void BuyItem(Item item)
        {
            GameManager.Instance.InventoryManager.BuyItem(item);
        }
    }
}
