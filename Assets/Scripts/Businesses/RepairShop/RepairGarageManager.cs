using System;
using Cars;
using Core;
using InventorySystem;
using UnityEngine;

namespace Businesses.RepairShop
{
    public class RepairGarageManager : MonoBehaviour
    {
        public bool isAvailable = true;
        public CarMovement carMovement;
        private CarRepairNeeds _carRepairNeeds;

        public event Action OnRepair;
        
        public void GetCarMovement(CarMovement carMovement)
        {
            this.carMovement = carMovement;
            _carRepairNeeds = this.carMovement.GetComponent<CarRepairNeeds>();
            _carRepairNeeds.GeneratePartToRepair();
            OnRepair?.Invoke();
        }
        
        public void TryRepair()
        {
            bool canRepair = true;
            
            foreach (var part in _carRepairNeeds.requiredParts)
            {
                InventoryItem invItem = GameManager.Instance.InventoryManager.inventoryItems.Find(i => i.item == part);
                if (invItem == null || invItem.quantity <= 0)
                {
                    canRepair = false;
                    break;
                }
            }

            if (canRepair)
            {
                foreach (var part in _carRepairNeeds.requiredParts)
                {
                    GameManager.Instance.InventoryManager.UseItem(part);
                }
                RepairEnded();
            }

        }
        
        public void RepairEnded()
        {
            isAvailable = true;
            carMovement.currentState = CarState.ReversingOut;
            SaveSystem.SaveSystem.Save();
        }
    }
}
