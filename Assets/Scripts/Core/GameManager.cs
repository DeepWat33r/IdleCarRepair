using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int ActiveSaveSlot { get; private set; } = -1;

        public PlayerProgression PlayerProgression { get; set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetActiveSaveSlot(int slotIndex)
        {
            ActiveSaveSlot = slotIndex;
        }
        
    }
}
