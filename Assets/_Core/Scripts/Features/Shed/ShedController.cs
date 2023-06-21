using Tool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using CarGame;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly ResourcePath _viewShedPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourceShedPath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly ShedView _shedView;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryContext _inventoryContext;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            [NotNull] Transform placeForUi,
            [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _upgradeHandlersRepository = CreateHandlerRepository();
            _inventoryContext = CreateInventoryContext(placeForUi, _profilePlayer.Inventory);
            _shedView = LoadShedView(placeForUi);

            _shedView.Init(Apply, Back);
        }


        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, _profilePlayer.Inventory);
            AddContext(context);
            return context;
        }

        private UpgradeHandlersRepository CreateHandlerRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourceShedPath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadShedView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewShedPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            ButtonsLog($"Apply");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            ButtonsLog($"Back");
        }

        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }

        private void ButtonsLog(string btnName)
        {
            DefaultLog($"{btnName}. Current {nameof(_profilePlayer.CurrentCar.Speed)}: {_profilePlayer.CurrentCar.Speed}");
            DefaultLog($"{btnName}. Current {nameof(_profilePlayer.CurrentCar.JumpHeight)}: {_profilePlayer.CurrentCar.JumpHeight}");
        }

        private void DefaultLog(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}