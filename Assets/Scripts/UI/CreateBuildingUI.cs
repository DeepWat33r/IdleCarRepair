using System;
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
        
        private Buildings.Buildings _buildings;
        
        public void Awake()
        {
            PlayerProgression.OnLevelChanged += EnableGasStationButton;
            _buildings = GetComponent<Buildings.Buildings>();
        }

        public void Start()
        {
            repairShopCostText.text = $"\u20ac" + _buildings.repairShopCost;
            gasStationCostText.text = $"\u20ac" + _buildings.gasStationCost;
        }

        private void EnableGasStationButton()
        {
            if (GameManager.Instance.PlayerUnlocks.GasStationUnlocked)
            {
                buildGasStationUIButton.SetActive(true);
            }
        }
       
    }
}
