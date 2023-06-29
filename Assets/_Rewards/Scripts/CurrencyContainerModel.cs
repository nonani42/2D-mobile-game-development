using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Rewards
{
    class CurrencyContainerModel
    {
        public void SetValue(RewardType rewardType, CurrencySlotView view, int value)
        {
            view.SetData(value);
            PlayerPrefs.SetInt(rewardType.ToString(), value);
        }

        public void LoadValue(RewardType rewardType, CurrencySlotView view)
        {
            int temp = GetData(rewardType);
            view.SetData(temp);
        }

        public int GetData(RewardType rewardType)
        {
            return PlayerPrefs.GetInt(rewardType.ToString());
        }

        public void ChangeValue(RewardType rewardType, CurrencySlotView view, int value)
        {
            int currentAmount = GetData(rewardType);
            int totalSum = currentAmount + value;
            SetValue(rewardType, view, totalSum);
        }

        public void Destroy()
        {
            PlayerPrefs.Save();
        }
    }
}
