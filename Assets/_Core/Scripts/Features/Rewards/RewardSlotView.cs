using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal class RewardSlotView : MonoBehaviour
    {
        [SerializeField] private Image _originalBackground;
        [SerializeField] private Image _selectBackground;
        [SerializeField] private Image _iconCurrency;
        [SerializeField] private TMP_Text _textPeriod;
        [SerializeField] private TMP_Text _countReward;

        public RewardType RewardType { get; private set; }
        public int Value { get; private set; }


        public void SetData(RewardSlotConfig rewardSlot, int periodCount, bool isSelected, RewardPeriodType period)
        {
            _iconCurrency.sprite = rewardSlot.CurrencySlotConfig.Icon;
            _textPeriod.text = $"{period} {periodCount}";
            _countReward.text = rewardSlot.Value.ToString();

            RewardType = rewardSlot.CurrencySlotConfig.Type;
            Value = rewardSlot.Value;

            UpdateBackground(isSelected);
        }

        public void SetSelectedBackground(bool isSelect)
        {
            UpdateBackground(isSelect);
        }

        private void UpdateBackground(bool isSelect)
        {
            _originalBackground.gameObject.SetActive(!isSelect);
            _selectBackground.gameObject.SetActive(isSelect);
        }
    }
}