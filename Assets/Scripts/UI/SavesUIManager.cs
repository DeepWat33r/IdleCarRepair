using TMPro;
using UnityEngine;

namespace UI
{
    public class SavesUIManager : MonoBehaviour
    {
        public TMP_Text[] saveSlotTexts;
        public GameObject[] saveDeleteButtons;

        private void Start()
        {
            UpdateSaveSlotsUI();
        }

        public void UpdateSaveSlotsUI()
        {
            for (int i = 0; i < saveSlotTexts.Length; i++)
            {
                if (SaveSystem.SaveSystem.IsSaveExists(i+1))
                {
                    saveSlotTexts[i].text = $"Save {i + 1}";
                    saveDeleteButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    saveSlotTexts[i].text = $"New Save";
                    saveDeleteButtons[i].gameObject.SetActive(false);
                }
            }
        }

        public void DeleteSave(int saveSlot)
        {
            SaveSystem.SaveSystem.DeleteSave(saveSlot);
            UpdateSaveSlotsUI();
        }
        
    }
}
