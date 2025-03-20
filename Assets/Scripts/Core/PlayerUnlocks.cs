using System;
using UnityEngine;

namespace Core
{
    public class PlayerUnlocks : MonoBehaviour
    {
        [SerializeField] public int levelToUnlockGasStation = 2;
        [SerializeField] public int levelToUnlockOilFilter = 2;
        [SerializeField] public int levelToUnlockEngine = 3;

        public bool GasStationUnlocked { get; private set; } = false;
        public bool OilFilterUnlocked  { get; private set; } = false;
        public bool EngineUnlocked { get; private set; } = false;
        
        public static event Action OnGasStationUnlocked;
        public static event Action OnOilFilterUnlocked;
        public static event Action OnEngineUnlocked;
        private void Awake()
        {
            GameManager.Instance.PlayerUnlocks = this;
            PlayerProgression.OnLevelChanged += CheckForUnlocks;
        }
        
        private void CheckForUnlocks()
        {
            if (GameManager.Instance.PlayerProgression.Level >= levelToUnlockGasStation)
            {
                GasStationUnlocked = true;
                OnGasStationUnlocked?.Invoke();
            }
            if (GameManager.Instance.PlayerProgression.Level >= levelToUnlockOilFilter)
            {
                OilFilterUnlocked = true;
                OnOilFilterUnlocked?.Invoke();
            }
            if (GameManager.Instance.PlayerProgression.Level >= levelToUnlockEngine)
            {
                EngineUnlocked = true;
                OnEngineUnlocked?.Invoke();
            }
        }
    }
}
