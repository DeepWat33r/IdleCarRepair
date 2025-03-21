using System;
using Core;
using UnityEngine;

namespace Businesses
{
    public class Buildings : MonoBehaviour
    {
        public GameObject gasStation;
        public GameObject repairShop;

        public int gasStationCost;
        public int repairShopCost;

        public bool isGasStationBuilt;
        public bool isRepairShopBuilt;
        
        private void Awake()
        {
            GameManager.Instance.Buildings = this;
        }

        private void Start()
        {
            CheckForBuildings();
        }

        public void BuildGasStation()
        {
            if (CanBuild(gasStationCost))
            {
                gasStation.SetActive(true);
                isGasStationBuilt = true;
                GameManager.Instance.PlayerProgression.SpendMoney(gasStationCost);
                SaveSystem.SaveSystem.Save();
            }
        }
        public void BuildRepairShop()
        {
            if (CanBuild(repairShopCost))
            {
                repairShop.SetActive(true);
                isRepairShopBuilt = true;
                GameManager.Instance.PlayerProgression.SpendMoney(repairShopCost);
                SaveSystem.SaveSystem.Save();
            }
        }
        public void CheckForBuildings()
        {
            if (isGasStationBuilt)
                gasStation.SetActive(true);
            if (isRepairShopBuilt)
                repairShop.SetActive(true);
        }
        public bool CanBuild(int buildingCost)
        {
            return GameManager.Instance.PlayerProgression.Money >= buildingCost;
        }
        #region Save/Load
        public void Save( ref BuildingsData data)
        {
            data.isGasStationBuilt = isGasStationBuilt;
            data.isRepairShopBuilt = isRepairShopBuilt;
        }
        public void Load(BuildingsData data)
        {
            isGasStationBuilt = data.isGasStationBuilt;
            isRepairShopBuilt = data.isRepairShopBuilt;
        }
        #endregion
    }
    [Serializable]
    public struct BuildingsData
    {
        public bool isGasStationBuilt;
        public bool isRepairShopBuilt;
    }
}
