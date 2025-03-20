using UnityEngine;
using System.IO;
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
