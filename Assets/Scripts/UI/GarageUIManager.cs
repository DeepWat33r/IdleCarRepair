using System;
using System.Collections.Generic;
using Businesses.RepairShop;
using Cars;
using Core;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GarageUIManager : MonoBehaviour
    {
        public GameObject garageRepairButton;
        public GameObject repairPartsPanel;
        public TMP_Text[] repairPartsText;
        public Color isPartAvailable;
        
        private RepairGarageManager _repairGarageManager;
        private CarRepairNeeds _carRepairNeeds;
        
        public void Start()
        {
            _repairGarageManager = GetComponent<RepairGarageManager>();
            _repairGarageManager.OnRepair += ShowNeededParts;
            InventoryManager.OnInventoryChanged += ShowNeededParts;
        }

        public void Update()
        {
            garageRepairButton.SetActive(!_repairGarageManager.isAvailable && _repairGarageManager.carMovement.currentState == CarState.Idle);
            if (!_repairGarageManager.isAvailable)
            {
                repairPartsPanel.SetActive(true);
            }
            else
            {
                repairPartsPanel.SetActive(false);
                foreach (TMP_Text text in repairPartsText)
                    text.text = "";
            }
            
        }

        private void ShowNeededParts()
        {
            if (_repairGarageManager.carMovement != null)
                _carRepairNeeds = _repairGarageManager.carMovement.GetComponent<CarRepairNeeds>();
            else
                return;
            
            int repairPartsCount = _carRepairNeeds.requiredParts.Count;
            for (int i = 0; i < repairPartsText.Length; i++)
            {
                if (i < repairPartsCount)
                {
                    repairPartsText[i].text = _carRepairNeeds.requiredParts[i].name;
                    repairPartsText[i].color = GameManager.Instance.InventoryManager.FindItem(_carRepairNeeds.requiredParts[i]) ? isPartAvailable : Color.red;
                }
                else
                    repairPartsText[i].text = "";
            }
        }
    }
}
