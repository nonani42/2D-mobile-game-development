using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Rewards.Currency
{
    class CurrencyController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Rewards/CurrencyView");
        private readonly ResourcePath _viewSlotPath = new ResourcePath("Prefabs/Rewards/CurrencySlotView");

        private CurrencySlotConfigDataSource _dataSource;

        private CurrencyView _view;
        private Transform _placeForCurrencySlots;

        private CurrencyModel _model;


        public CurrencyController(CurrencyModel model, Transform placeForUi)
        {
            _view = LoadView(placeForUi);
            _dataSource = _view.CurrencySlotConfigDataSource;
            _placeForCurrencySlots = _view.PlaceForCurrencySlots;
            _model = model;

            if (_placeForCurrencySlots == null)
                throw new ArgumentNullException(nameof(_placeForCurrencySlots));

            Subscribe();
            Init();
        }

        private void Subscribe()
        {
            _model.OnValueChanged += _view.ChangeValue;
        }

        private void Unsubscribe()
        {
            _model.OnValueChanged -= _view.ChangeValue;
        }


        private CurrencyView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CurrencyView>();
        }

        protected override void OnDispose()
        {
            Deinit();
            Unsubscribe();
            base.OnDispose();
        }

        private void Init()
        {
            LoadCurrencySlotView(_placeForCurrencySlots, _view);

            foreach (var slotView in _view.CurrencySlotViews)
            {
                _model.LoadValue(slotView.Key);
            }
        }

        private void Deinit()
        {
            DestroyCurrencySlotView();
        }

        private void LoadCurrencySlotView(Transform placeForCurrencySlots, CurrencyView currencyView)
        {
            ICurrency[] currencySlotConfigs = _dataSource.CurrencySlotConfigs;
            foreach (ICurrency config in currencySlotConfigs)
            {
                CurrencySlotView view = CreateCurrencySlotView(placeForCurrencySlots);
                view.SetIcon(config.Icon);
                currencyView.CurrencySlotViews.Add(config.Type, view);
            }
        }

        private void DestroyCurrencySlotView()
        {
            foreach (var view in _view.CurrencySlotViews)
            {
                if(view.Value != null)
                    Object.Destroy(view.Value.gameObject);
            }
            _view.CurrencySlotViews.Clear();
        }

        private CurrencySlotView CreateCurrencySlotView(Transform placeForCurrencySlots)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewSlotPath);
            GameObject gameObject = Object.Instantiate(prefab, placeForCurrencySlots);
            CurrencySlotView view = gameObject.GetComponent<CurrencySlotView>();
            return view;
        }
    }
}
