using System;
using System.Data.SqlTypes;
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
        
        public Color canBuildColor;
        
        private Buildings _buildings;
        
        public void Awake()
        {
            PlayerProgression.OnLevelChanged += EnableGasStationButton;
            PlayerProgression.OnMoneyChanged += CostColorText;
            _buildings = GetComponent<Buildings>();
        }

        public void Start()
        {
            repairShopCostText.text = $"\u20ac" + _buildings.repairShopCost;
            gasStationCostText.text = $"\u20ac" + _buildings.gasStationCost;
            
            CostColorText(0);
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
        public void DisableGasStationButton()
        {
            if (GameManager.Instance.Buildings.isGasStationBuilt)
            {
                buildGasStationUIButton.SetActive(false);
            }
        }
        public void DisableRepairShopButton()
        {
            if (GameManager.Instance.Buildings.isRepairShopBuilt)
            {
                repairShopUIButton.SetActive(false);
            }
        }

        private void CostColorText(int money)
        {
            int i = money;
            gasStationCostText.color = GameManager.Instance.PlayerProgression.Money >= _buildings.gasStationCost ? canBuildColor : Color.red;
            repairShopCostText.color = GameManager.Instance.PlayerProgression.Money >= _buildings.repairShopCost ? canBuildColor : Color.red;
        }
    }
}
