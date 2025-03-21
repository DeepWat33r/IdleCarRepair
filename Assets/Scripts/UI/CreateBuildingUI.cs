using System;
using Businesses;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CreateBuildingUI : MonoBehaviour
    {
        public GameObject repairShopUIButton;
        public GameObject buildGasStationUIButton;
        
        public TMP_Text repairShopCostText;
        public TMP_Text gasStationCostText;
        
        private Buildings _buildings;
        
        public void Awake()
        {
            PlayerProgression.OnLevelChanged += EnableGasStationButton;
            _buildings = GetComponent<Buildings>();
        }

        public void Start()
        {
            repairShopCostText.text = $"\u20ac" + _buildings.repairShopCost;
            gasStationCostText.text = $"\u20ac" + _buildings.gasStationCost;
            
            DisableRepairShopButton(); 
            DisableGasStationButton();
        }

        private void EnableGasStationButton()
        {
            if (GameManager.Instance.PlayerUnlocks.GasStationUnlocked)
            {
                buildGasStationUIButton.SetActive(true);
            }
        }
        private void DisableGasStationButton()
        {
            if (GameManager.Instance.Buildings.isGasStationBuilt)
            {
                buildGasStationUIButton.SetActive(false);
            }
        }
        private void DisableRepairShopButton()
        {
            if (GameManager.Instance.Buildings.isRepairShopBuilt)
            {
                repairShopUIButton.SetActive(false);
            }
        }
       
    }
}
