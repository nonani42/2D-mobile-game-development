using System;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyModel
    {
        public event Action<RewardType, int> OnValueChanged;

        private void SetData(RewardType rewardType, int value)
        {
            PlayerPrefs.SetInt(rewardType.ToString(), value);
        }

        private int GetData(RewardType rewardType)
        {
            return PlayerPrefs.GetInt(rewardType.ToString());
        }

        public void LoadValue(RewardType rewardType)
        {
            int currentValue = GetData(rewardType);

            OnValueChanged?.Invoke(rewardType, currentValue);
        }

        public void ChangeValue(RewardType rewardType, int value)
        {
            int currentValue = GetData(rewardType);
            int newValue = currentValue + value;

            SetData(rewardType, newValue);
            OnValueChanged?.Invoke(rewardType, newValue);
        }
    }
}
