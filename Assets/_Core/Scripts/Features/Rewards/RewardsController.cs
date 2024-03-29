﻿using Features.Rewards.Currency;
using Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Rewards
{
    internal class RewardsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Rewards/RewardsView");

        private readonly RewardsView _view;
        private RewardsModel _model;
        private readonly RewardSlotConfigDataSource _rewardSlotConfigDataSource;

        private PlayerProfile _profilePlayer;

        private readonly float _timeDeadline;
        private readonly float _timeCoolDown;

        private List<RewardSlotView> _slots;
        private Coroutine _coroutine;

        private bool _isGetReward;


        public RewardsController(Transform placeForUi, PlayerProfile profilePlayer)
        {
            _view = LoadView(placeForUi);

            CreateCurrencyController(placeForUi, profilePlayer.Currency);

            _rewardSlotConfigDataSource = _view.RewardSlotConfigDataSource;

            _profilePlayer = profilePlayer;

            _timeDeadline = _rewardSlotConfigDataSource.TimeDeadline;
            _timeCoolDown = _rewardSlotConfigDataSource.TimeCooldown;

            Init();
        }

        private RewardsView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<RewardsView>();
        }

        private CurrencyController CreateCurrencyController(Transform placeForUi, CurrencyModel currencyModel)
        {
            var currencyController = new CurrencyController(currencyModel, placeForUi);
            AddController(currencyController);

            return currencyController;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Deinit();
        }

        public void Init()
        {
            _model = new RewardsModel(_rewardSlotConfigDataSource.Period);
            InitSlots();
            RefreshUi();
            _view.SubscribeButtons(ClaimReward, ResetRewardsState);
            StartRewardsUpdating();
        }

        public void Deinit()
        {
            DeinitSlots();
            StopRewardsUpdating();
            _view.UnsubscribeButtons();
        }


        private void InitSlots()
        {
            _slots = new List<RewardSlotView>();

            for (int i = 0; i < _rewardSlotConfigDataSource.RewardSlotConfigs.Length; i++)
            {
                RewardSlotView instanceSlot = CreateSlotRewardView();

                RewardSlotConfig rewardSlot = _rewardSlotConfigDataSource.RewardSlotConfigs[i];
                int countPeriod = i + 1;
                bool isSelected = i == _model.CurrentSlotInActive;
                instanceSlot.SetData(rewardSlot, countPeriod, isSelected, _rewardSlotConfigDataSource.Period);

                _slots.Add(instanceSlot);
            }
        }

        private void DeinitSlots()
        {
            foreach (RewardSlotView slot in _slots)
            {
                if(slot != null)
                    Object.Destroy(slot.gameObject);
            }

            _slots.Clear();
        }

        private RewardSlotView CreateSlotRewardView() =>
            Object.Instantiate
            (
                _view.SlotPrefab,
                _view.SlotsRewardContainer,
                false
            );

        private void StartRewardsUpdating() =>
            _coroutine = _view.StartCoroutine(RewardsStateUpdater());

        private void StopRewardsUpdating()
        {
            if (_coroutine == null)
                return;

            _view.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RewardsStateUpdater()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1);

            while (true)
            {
                RefreshRewardsState();
                RefreshUi();
                yield return waitForSecond;
            }
        }

        private void RefreshRewardsState()
        {
            bool gotRewardEarlier = _model.TimeGetReward.HasValue;
            
            if (!gotRewardEarlier)
            {
                _isGetReward = true;
                return;
            }

            TimeSpan timeFromLastRewardGetting =
                DateTime.UtcNow - _model.TimeGetReward.Value;

            bool isDeadlineElapsed =
                timeFromLastRewardGetting.Seconds >= _timeDeadline;

            bool isTimeToGetNewReward =
                timeFromLastRewardGetting.Seconds >= _timeCoolDown;

            bool isLastReward =
                _model.CurrentSlotInActive >= _slots.Count;

            if (isLastReward)
                ResetRewardsCycle();

            if (isDeadlineElapsed)
                ResetRewardsState();

            _isGetReward = isTimeToGetNewReward;
        }

        private void ClaimReward()
        {
            if (!_isGetReward)
                return;

            RewardSlotView reward = _slots[_model.CurrentSlotInActive];
            _profilePlayer.Currency.ChangeValue(reward.RewardType, reward.Value);
            _model.UpdateRewardSave();

            RefreshRewardsState();
        }

        private void ResetRewardsState()
        {
            _model.TimeGetReward = null;
            ResetRewardsCycle();
        }

        private void ResetRewardsCycle()
        {
            _model.CurrentSlotInActive = 0;
            RefreshSlotsBackground();
        }

        private void RefreshSlotsBackground()
        {
            foreach (RewardSlotView slot in _slots)
            {
                slot.SetSelectedBackground(false);
            }
            _slots[_model.CurrentSlotInActive].SetSelectedBackground(true);
        }

        private void RefreshUi()
        {
            _view.ActivateRewardButton(_isGetReward);
            _view.UpdateTimerText(_isGetReward, GetTime());
            RefreshSlotsBackground();
        }

        private TimeSpan GetTime()
        {
            TimeSpan currentClaimCooldown;
            if (_model.TimeGetReward.HasValue)
            {
                DateTime nextClaimTime = _model.TimeGetReward.Value.AddSeconds(_timeCoolDown);
                currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
            }
            return currentClaimCooldown;
        }
    }
}
