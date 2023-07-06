using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Rewards.Currency
{
    internal class CurrencyView : MonoBehaviour
    {
        [field: Header("Configs")]
        [field: SerializeField] public CurrencySlotConfigDataSource CurrencySlotConfigDataSource { get ; private set ; }

        [field: SerializeField] public Transform PlaceForCurrencySlots { get; private set; }

        private Dictionary<RewardType, CurrencySlotView> _currencySlotViews = new Dictionary<RewardType, CurrencySlotView>();

        internal Dictionary<RewardType, CurrencySlotView> CurrencySlotViews { get => _currencySlotViews; set => _currencySlotViews = value; }


        internal void ChangeValue(RewardType rewardType, int value)
        {
            if (CurrencySlotViews.TryGetValue(rewardType, out CurrencySlotView slotView))
            {
                slotView.SetData(value);
            }
            else
            {
                throw new ArgumentNullException("No Such Currency");
            }
        }
    }
}
