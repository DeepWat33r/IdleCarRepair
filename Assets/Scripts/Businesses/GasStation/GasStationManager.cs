using System;
using Cars;
using Core;
using UnityEngine;
using Utils;

namespace Businesses.GasStation
{
    public class GasStationManager : MonoBehaviour
    {
        public bool isAvailable = true;
        public float timeToRefuel = 10f;
        
        private CarMovement _carMovement;
        private Timer _waitTimer;
        private PlayerProgression _playerProgression;
        public void Start()
        {
            _playerProgression = GameManager.Instance.PlayerProgression;
        }

        public void Update()
        {
            if (_waitTimer != null)
            {
                if (_waitTimer.isActive)
                {
                    _waitTimer.Update();
                }
                else
                {
                    _waitTimer = null; 
                }
                return;
            }

            if (!isAvailable && _carMovement != null)
            {
                WaitForRefuel(RefuelingCompleted);
            }
        }

        public void GetCarMovement(CarMovement carMovement)
        {
            _carMovement = carMovement;
        }

        public void RefuelingCompleted()
        {
            isAvailable = true;
            _carMovement.currentState = CarState.ReversingOut;
            _playerProgression.AddMoney(50);
            SaveSystem.SaveSystem.Save();
        }
        
        private void WaitForRefuel(Action onComplete)
        {
            _waitTimer = new Timer(timeToRefuel, onComplete);
            _waitTimer.isActive = true;
        }
    }
}
