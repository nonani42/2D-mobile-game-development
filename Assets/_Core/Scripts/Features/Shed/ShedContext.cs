using CarGame;
using Features.Inventory;
using Features.Shed.Upgrade;
using System;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace Features.Shed
{

    internal class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewShedPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourceShedPath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly InventoryContext _inventoryContext;


        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            CreateController(placeForUi, profilePlayer);
        }


        private ShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            IShedView view = LoadShedView(placeForUi);
            IUpgradeHandlersRepository repository = CreateHandlerRepository();

            var shedController = new ShedController(profilePlayer, view, repository);
            AddController(shedController);
            return shedController;
        }

        private UpgradeHandlersRepository CreateHandlerRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourceShedPath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private IShedView LoadShedView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewShedPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);
            return context;
        }
    }
}