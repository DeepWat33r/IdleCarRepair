using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Slider expSlider;
        
        private void OnEnable()
        {
            PlayerProgression.OnMoneyChanged += UpdateMoneyUI;
            PlayerProgression.OnLevelChanged += UpdateLevelUI;
            PlayerProgression.OnExperienceChanged += UpdateExpUI;

            SyncAll();
        }

        private void OnDisable()
        {
            PlayerProgression.OnMoneyChanged -= UpdateMoneyUI;
            PlayerProgression.OnLevelChanged -= UpdateLevelUI;
            PlayerProgression.OnExperienceChanged -= UpdateExpUI;
        }

        private void UpdateMoneyUI(int amount)
        {
            moneyText.text = $"{amount}";
        }

        private void UpdateLevelUI(int level)
        {
            levelText.text = $"{level}";
            UpdateExpSlider();
        }

        private void UpdateExpUI(int currentExp)
        {
            UpdateExpSlider();
        }

        private void UpdateExpSlider()
        {
            var player = PlayerProgression.Instance;

            int nextLevel = Mathf.Clamp(player.Level, 1,  player.Level);
            int maxExpForThisLevel = player.Level >= player.MaxLevel ? 1 : player.GetExpRequirementForLevel(player.Level);

            expSlider.maxValue = maxExpForThisLevel;
            expSlider.value = player.Experience;
        }

        private void SyncAll()
        {
            if (PlayerProgression.Instance == null)
            {
                Debug.LogWarning("PlayerProgression.Instance is not ready yet.");
                return;
            }
            UpdateMoneyUI(PlayerProgression.Instance.Money);
            UpdateLevelUI(PlayerProgression.Instance.Level);
            UpdateExpUI(PlayerProgression.Instance.Experience);
        }
    }
}
