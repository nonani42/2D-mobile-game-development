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
        private readonly IShedView _shedView;
        private readonly ProfilePlayer _profilePlayer;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;


        public ShedController(
            [NotNull] ProfilePlayer profilePlayer,
            [NotNull] IShedView shedView,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository)
        {
            _profilePlayer =
                profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _shedView = 
                shedView ?? throw new ArgumentNullException(nameof(shedView));

            _upgradeHandlersRepository = 
                upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));

            _shedView.Init(Apply, Back);
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