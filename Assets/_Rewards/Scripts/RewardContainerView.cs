using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rewards
{
    internal class RewardContainerView : MonoBehaviour
    {
        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform SlotsRewardContainer { get; private set; }
        [field: SerializeField] public RewardSlotView SlotPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        [field: Header("Timer messages")]
        [field: SerializeField] public string RewardMessage { get; private set; } = "The reward is ready to be received!";
        [field: SerializeField] public string TimerMessage { get; private set; } = $"Time till the next reward: ";

        private UnityAction _rewardButton;
        private UnityAction _resetButton;

        public void SubscribeButtons(UnityAction rewardButton, UnityAction resetButton)
        {
            _rewardButton = rewardButton;
            _resetButton = resetButton;

            GetRewardButton.onClick.AddListener(_rewardButton);
            ResetButton.onClick.AddListener(_resetButton);
        }

        public void UnsubscribeButtons()
        {
            GetRewardButton.onClick.RemoveListener(_rewardButton);
            ResetButton.onClick.RemoveListener(_resetButton);
        }

        public void ActivateRewardButton(bool interactable)
        {
            GetRewardButton.interactable = interactable;
        }

        public void UpdateTimerText(bool isRewardTime, TimeSpan timeOnTimer)
        {
            if (isRewardTime)
            {
                TimerNewReward.text = $"{RewardMessage}";
            }

            if (timeOnTimer.TotalSeconds > 0)
            { 
                string time =
                    $"{timeOnTimer.Days:D2}:{timeOnTimer.Hours:D2}:" +
                    $"{timeOnTimer.Minutes:D2}:{timeOnTimer.Seconds:D2}";

                TimerNewReward.text = $"{TimerMessage}{time}";
            }
        }
    }
}