using System;
using Cars;
using UnityEngine;

namespace RepairShop
{
    public class RepairGarageManager : MonoBehaviour
    {
        public bool isAvailable = true;
        private CarMovement _carMovement;
        
        public void Update()
        {

        }

        public void GetCarMovement(CarMovement carMovement)
        {
            _carMovement = carMovement;
        }

        public void RepairEnded()
        {
            isAvailable = true;
            _carMovement.currentState = CarState.ReversingOut;
        }
    }
}
