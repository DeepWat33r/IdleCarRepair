using System;
using UnityEngine;

namespace Core
{
    public class PlayerProgression : MonoBehaviour
    {
        //public static PlayerProgression Instance { get; set; }

        [Header("Player Data")]
        [SerializeField] private int level = 1;
        [SerializeField] private int experience = 0;
        [SerializeField] private int money = 1000;

        [Header("Level Config")]
        [SerializeField] private int maxLevel = 5;
        [SerializeField] private int[] expPerLevel = { 0, 100, 250, 500, 1000 };

        public int Level => level;
        public int Experience => experience;
        public int Money => money;

        public static event Action<int> OnMoneyChanged;
        public static event Action<int> OnExperienceChanged;
        public static event Action<int> OnLevelChanged;

        private void Awake()
        {
            GameManager.Instance.PlayerProgression = this;
        }

        public void AddMoney(int amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
        }

        public bool SpendMoney(int amount)
        {
            if (money >= amount)
            {
                money -= amount;
                OnMoneyChanged?.Invoke(money);
                return true;
            }
            return false;
        }

        public void AddExperience(int amount)
        {
            if (level >= maxLevel) return;

            experience += amount;
            OnExperienceChanged?.Invoke(experience);

            while (level < maxLevel && experience >= expPerLevel[level])
            {
                experience -= expPerLevel[level];
                level++;
                OnLevelChanged?.Invoke(level);
            }
        }
        
        public int GetExpRequirementForLevel(int lvl)
        {
            return lvl < maxLevel ? expPerLevel[lvl] : 1;
        }

        public int MaxLevel => maxLevel;
        
        public void LoadPlayerData(int loadedLevel, int loadedExp, int loadedMoney)
        {
            level = Mathf.Clamp(loadedLevel, 1, maxLevel);
            experience = loadedExp;
            money = loadedMoney;

            OnMoneyChanged?.Invoke(money);
            OnExperienceChanged?.Invoke(experience);
            OnLevelChanged?.Invoke(level);
        }

        #region Save/Load
        public void Save( ref PlayerProgressionData data)
        {
            data.level = level;
            data.experience = experience;
            data.money = money;
        }
        public void Load(PlayerProgressionData data)
        {
            level = Mathf.Clamp(data.level, 1, maxLevel);
            experience = data.experience;
            money = data.money;
        }
        #endregion
    }
    
    [Serializable]
    public struct PlayerProgressionData
    {
        public int level;
        public int experience;
        public int money;
    }
}
