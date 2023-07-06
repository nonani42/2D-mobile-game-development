using System;
using System.Collections.Generic;
using UnityEngine;
using Features.Shed.Upgrade;
using JetBrains.Annotations;
using Profile;
using Features.Inventory;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly IShedView _shedView;
        private readonly ProfilePlayer _playerProfile;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;

        public List<string> _rollbackInventory;

        public List<string> RollbackInventory 
        { 
            get 
            {
                if (_rollbackInventory == null)
                    _rollbackInventory = new List<string>();
                return _rollbackInventory; 
            }
            set
            {
                _rollbackInventory = new List<string>();
                foreach (var item in value)
                {
                    _rollbackInventory.Add(item);
                }
            }
        }

        public ShedController(
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IShedView shedView,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository)
        {
            _playerProfile =
                profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            RollbackInventory = (List<string>)_playerProfile.Inventory.EquippedItems;

            _shedView =
                shedView ?? throw new ArgumentNullException(nameof(shedView));

            _upgradeHandlersRepository =
                upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));

            _shedView.Init(Apply);
        }

        protected override void OnDispose()
        {
            RollbackEqippedItems();

            UpgradeLog();

            _shedView.Deinit();
            base.OnDispose();
        }

        private void Apply()
        {
            _playerProfile.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _playerProfile.CurrentCar,
                _playerProfile.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            UpgradeLog();
            RollbackInventory = (List<string>)_playerProfile.Inventory.EquippedItems;
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

        private void RollbackEqippedItems()
        {
            _playerProfile.Inventory.EquipSelected(RollbackInventory);
        }

        private void UpgradeLog()
        {
            DefaultLog($"Current {nameof(_playerProfile.CurrentCar.Speed)}: {_playerProfile.CurrentCar.Speed}");
            DefaultLog($"Current {nameof(_playerProfile.CurrentCar.JumpHeight)}: {_playerProfile.CurrentCar.JumpHeight}");
        }

        private void DefaultLog(string message) =>
            Debug.Log($"[{GetType().Name}] {message}");
    }
}