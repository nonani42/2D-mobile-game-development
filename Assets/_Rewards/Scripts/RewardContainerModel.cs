using System;
using UnityEngine;

namespace Rewards
{
    internal class RewardContainerModel
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetRewardKey = nameof(TimeGetRewardKey);

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                if (!string.IsNullOrEmpty(data))
                {
                    return DateTime.Parse(data);
                }
                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }

        public void Destroy()
        {
            PlayerPrefs.Save();
        }

        public void UpdateRewardSave()
        {
            TimeGetReward = DateTime.UtcNow;
            CurrentSlotInActive++;
        }
    }
}