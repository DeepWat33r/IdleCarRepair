using System;
using UnityEngine;

namespace Cars.Zones
{
    public class CarRepairZone : MonoBehaviour
    {
        [SerializeField] private Transform facilityTargetPosition;
        [SerializeField] private Transform roadReturnPosition;
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                var car = other.GetComponent<CarMovement>();
                if (car != null && car.currentState == CarState.OnRoad && car.isNeedRepair)
                {
                    car.isNeedRepair = false;
                    car.TurnToFacility(facilityTargetPosition, roadReturnPosition);
                }
            }
        }
    }
}
