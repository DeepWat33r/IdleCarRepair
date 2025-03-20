using System;
using Cars;
using Core;
using UnityEngine;

namespace RepairShop
{
    public class RepairGarageManager : MonoBehaviour
    {
        public bool isAvailable = true;
        private CarMovement _carMovement;

        private PlayerProgression _playerProgression;
        public void Start()
        {
            _playerProgression = PlayerProgression.Instance;
        }

        public void GetCarMovement(CarMovement carMovement)
        {
            _carMovement = carMovement;
        }

        public void RepairEnded()
        {
            isAvailable = true;
            _carMovement.currentState = CarState.ReversingOut;
            _playerProgression.AddMoney(100);
            _playerProgression.AddExperience(50);
        }
    }
}
