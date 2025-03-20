using System;
using UnityEngine;

namespace Core
{
    public class PlayerProgression : MonoBehaviour
    {
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
        public static event Action OnLevelChanged;

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
                OnLevelChanged?.Invoke();
            }
        }
        
        public int GetExpRequirementForLevel(int lvl)
        {
            return lvl < maxLevel ? expPerLevel[lvl] : 1;
        }

        public int MaxLevel => maxLevel;
        
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
            
            OnMoneyChanged?.Invoke(money);
            OnExperienceChanged?.Invoke(experience);
            OnLevelChanged?.Invoke();
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
