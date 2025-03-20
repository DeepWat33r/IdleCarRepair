using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Slider expSlider;
        [SerializeField] private int mainMenuSceneIndex = 0;

        private PlayerProgression _playerProgression;
        public void Awake()
        {
            _playerProgression = GameManager.Instance.PlayerProgression;
        }

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
            
            int nextLevel = Mathf.Clamp(_playerProgression.Level, 1,  _playerProgression.Level);
            int maxExpForThisLevel = _playerProgression.Level >= _playerProgression.MaxLevel ? 1 : _playerProgression.GetExpRequirementForLevel(_playerProgression.Level);

            expSlider.maxValue = maxExpForThisLevel;
            expSlider.value = _playerProgression.Experience;
        }

        private void SyncAll()
        {
            if (_playerProgression == null)
            {
                Debug.LogWarning("PlayerProgression.Instance is not ready yet.");
                return;
            }
            UpdateMoneyUI(_playerProgression.Money);
            UpdateLevelUI(_playerProgression.Level);
            UpdateExpUI(_playerProgression.Experience);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadSceneAsync(mainMenuSceneIndex);
        }
    }
}
