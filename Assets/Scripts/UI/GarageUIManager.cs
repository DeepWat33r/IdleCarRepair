using System;
using RepairShop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GarageUIManager : MonoBehaviour
    {
        public GameObject garageRepairButton;

        private RepairGarageManager _repairGarageManager;
        
        public void Start()
        {
            _repairGarageManager = GetComponent<RepairGarageManager>();
        }

        public void Update()
        {
            garageRepairButton.SetActive(!_repairGarageManager.isAvailable);
        }
        
    }
}
