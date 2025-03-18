using System;
using RepairShop;
using UnityEngine;

namespace Cars.Zones
{
    public class CarRepairZone : MonoBehaviour
    {
        
        [SerializeField] private Transform facilityTargetPosition;
        [SerializeField] private Transform roadReturnPosition;

        private RepairGarageManager _repairGarageManager;
        public void Start()
        {
            _repairGarageManager = GetComponentInParent<RepairGarageManager>();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                var car = other.GetComponent<CarMovement>();
                if (car != null && car.currentState == CarState.OnRoad && car.isNeedRepair && _repairGarageManager.isAvailable)
                {
                    car.isNeedRepair = false;
                    car.TurnToFacility(facilityTargetPosition, roadReturnPosition);
                    _repairGarageManager.isAvailable = false;
                    _repairGarageManager.GetCarMovement(car);
                }
            }
        }
        
    }
}
