using UnityEngine;
using System.IO;
using Businesses;
using Core;

namespace SaveSystem
{
    public class SaveSystem
    {
        private static SaveData _saveData = new SaveData();

        [System.Serializable]
        public struct SaveData
        {
            public PlayerProgressionData playerProgressionSaveData;
            public PlayerUnlocksData playerUnlocksSaveData;
            public BuildingsData buildingsSaveData;
        }

        public static string SaveFileName(int saveSlot)
        {
            string saveFile = Application.persistentDataPath + "/saveData" + saveSlot + ".json";
            return saveFile;
        }

        public static void Save()
        {
            HandleSaveData();
            
            File.WriteAllText(SaveFileName(GameManager.Instance.ActiveSaveSlot), JsonUtility.ToJson(_saveData, true));
        }

        public static void HandleSaveData()
        {
            GameManager.Instance.PlayerProgression.Save(ref _saveData.playerProgressionSaveData);
            GameManager.Instance.PlayerUnlocks.Save(ref _saveData.playerUnlocksSaveData);
            GameManager.Instance.Buildings.Save(ref _saveData.buildingsSaveData);
        }

        public static void Load()
        {
            string saveContent = File.ReadAllText(SaveFileName(GameManager.Instance.ActiveSaveSlot));
            
            _saveData = JsonUtility.FromJson<SaveData>(saveContent);
            HandleLoadData();
        }

        public static void HandleLoadData()
        {
            GameManager.Instance.PlayerProgression.Load(_saveData.playerProgressionSaveData);
            GameManager.Instance.PlayerUnlocks.Load(_saveData.playerUnlocksSaveData);
            GameManager.Instance.Buildings.Load(_saveData.buildingsSaveData);
        }
        
        public static bool IsSaveExists(int saveSlot)
        {
            string path = SaveFileName(saveSlot);
            return File.Exists(path);
        }

        public static void DeleteSave(int saveSlot)
        {
            string path = SaveFileName(saveSlot);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
