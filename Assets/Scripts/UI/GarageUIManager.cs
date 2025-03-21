using System;
using Businesses.RepairShop;
using Cars;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GarageUIManager : MonoBehaviour
    {
        public GameObject garageRepairButton;
        public GameObject repairPartsPanel;
        public TMP_Text repairPartsText;
        
        private RepairGarageManager _repairGarageManager;
        private CarRepairNeeds _carRepairNeeds;
        
        public void Start()
        {
            _repairGarageManager = GetComponent<RepairGarageManager>();
            _repairGarageManager.OnRepair += ShowNeededParts;
        }

        public void Update()
        {
            garageRepairButton.SetActive(!_repairGarageManager.isAvailable);
            if (!_repairGarageManager.isAvailable)
            {
                repairPartsPanel.SetActive(true);
            }
            else
            {
                repairPartsPanel.SetActive(false);
                repairPartsText.text = "";
            }
            
        }
        public void ShowNeededParts()
        {
            _carRepairNeeds = _repairGarageManager.carMovement.GetComponent<CarRepairNeeds>();
            foreach (var part in _carRepairNeeds.requiredParts)
            {
                repairPartsText.text += part.name + "\n";
            }
        }
    }
}
