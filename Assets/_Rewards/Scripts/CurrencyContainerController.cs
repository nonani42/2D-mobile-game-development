using CarGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    class CurrencyContainerController
    {
        private readonly ResourcePath _viewSlotPath = new ResourcePath("Prefabs/CurrencySlotView");
        private CurrencySlotConfigDataSource _dataSource;

        private CurrencyContainerView _view;
        private Transform _placeForCurrencySlots;

        private CurrencyContainerModel _model;

        private Dictionary<RewardType, CurrencySlotView> _currencySlotViews = new Dictionary<RewardType, CurrencySlotView>();


        public CurrencyContainerController(CurrencyContainerView view, CurrencySlotConfigDataSource dataSource)
        {
            _dataSource = dataSource;
            _view = view;
            _placeForCurrencySlots = _view.placeForCurrencySlots;

            if (_placeForCurrencySlots == null)
                throw new ArgumentNullException(nameof(_placeForCurrencySlots));
        }

        public void Init()
        {
            _model = new CurrencyContainerModel();
            LoadCurrencySlotView(_placeForCurrencySlots);
            LoadCurrency();
        }

        public void Deinit()
        {
            _model.Destroy();
            DestroyCurrencySlotView();
        }

        private void LoadCurrencySlotView(Transform placeForCurrencySlots)
        {
            ICurrency[] currencySlotConfigs = _dataSource.CurrencySlotConfigs;
            foreach (ICurrency config in currencySlotConfigs)
            {
                CurrencySlotView view = CreateCurrencySlotView(placeForCurrencySlots);
                view.SetIcon(config.Icon);
                _currencySlotViews.Add(config.Type, view);
            }
        }

        private void DestroyCurrencySlotView()
        {
            foreach (var view in _currencySlotViews)
            {
                if(view.Value != null)
                    Object.Destroy(view.Value.gameObject);
            }
            _currencySlotViews.Clear();
        }

        private CurrencySlotView CreateCurrencySlotView(Transform placeForCurrencySlots)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewSlotPath);
            GameObject gameObject = Object.Instantiate(prefab, placeForCurrencySlots);
            CurrencySlotView view = gameObject.GetComponent<CurrencySlotView>();
            return view;
        }

        private void LoadCurrency()
        {
            foreach (var currency in _currencySlotViews)
            {
                _model.LoadValue(currency.Key, currency.Value);
            }
        }

        public void ChangeCurrency(RewardType rewardType, int value)
        {
            CurrencySlotView temp;

            if(_currencySlotViews.TryGetValue(rewardType, out temp))
            {
                _model.ChangeValue(rewardType, temp, value);
            }
            else
            {
                throw new ArgumentNullException("No Such Currency");
            }
        }
    }
}
