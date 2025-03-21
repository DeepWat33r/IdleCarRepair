using System;
using Businesses.GasStation;
using Businesses.RepairShop;
using UnityEngine;

namespace Cars.Zones
{
    public class CarTurnZone : MonoBehaviour
    {
        
        [SerializeField] private Transform facilityTargetPosition;
        [SerializeField] private Transform roadReturnPosition;

        private RepairGarageManager _repairGarageManager;
        private GasStationManager _gasStationManager;
        public void Start()
        {
            if (GetComponentInParent<RepairGarageManager>() != null)
                _repairGarageManager = GetComponentInParent<RepairGarageManager>();
            if (GetComponentInParent<GasStationManager>() != null)
                _gasStationManager = GetComponentInParent<GasStationManager>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                var car = other.GetComponent<CarMovement>();
                if (car != null && _repairGarageManager!=null && car.currentState == CarState.OnRoad && car.isNeedRepair && _repairGarageManager.isAvailable)
                {
                    car.isNeedRepair = false;
                    car.TurnToFacility(facilityTargetPosition, roadReturnPosition);
                    _repairGarageManager.isAvailable = false;
                    _repairGarageManager.GetCarMovement(car);
                }
                if (car != null && _gasStationManager!=null && car.currentState == CarState.OnRoad && car.isNeedFuel && _gasStationManager.isAvailable)
                {
                    car.isNeedFuel = false;
                    car.TurnToFacility(facilityTargetPosition, roadReturnPosition);
                    _gasStationManager.isAvailable = false;
                    _gasStationManager.GetCarMovement(car);
                }
            }
        }
        
    }
}
