using System;
using Core;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public void Start()
        {
            int saveSlot = GameManager.Instance.ActiveSaveSlot;
            if (saveSlot < 0)
                return;
            
            if (SaveSystem.IsSaveExists(saveSlot))
                SaveSystem.Load();
            else
                SaveSystem.Save();
        }
    }
}
