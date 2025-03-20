using Core;
using UnityEngine;

namespace Buildings
{
    public class Buildings : MonoBehaviour
    {
        public GameObject gasStation;
        public GameObject repairShop;

        public int gasStationCost;
        public int repairShopCost;
        
        public void BuildGasStation()
        {
            if(CanBuild(gasStationCost))
                gasStation.SetActive(true);
        }
        public void BuildRepairShop()
        {
            if(CanBuild(repairShopCost))
                repairShop.SetActive(true);
        }

        public bool CanBuild(int buildingCost)
        {
            return GameManager.Instance.PlayerProgression.Money >= buildingCost;
        }
    }
}
