using Cars;
using Core;
using UnityEngine;

namespace Businesses.RepairShop
{
    public class RepairGarageManager : MonoBehaviour
    {
        public bool isAvailable = true;
        private CarMovement _carMovement;
        
        private PlayerProgression _playerProgression;
        public void Start()
        {
            _playerProgression = GameManager.Instance.PlayerProgression;
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
            SaveSystem.SaveSystem.Save();
        }
    }
}
