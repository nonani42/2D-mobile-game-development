using CarGame;
using Features.Inventory.Items;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Inventory
{
    internal class InventoryContext : BaseContext
    {
        private readonly ResourcePath _viewInventoryPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourceInventoryPath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
        public readonly InventoryController inventoryController;

        public InventoryContext(Transform placeForUi, IInventoryModel model)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (model == null)
                throw new ArgumentNullException(nameof(model));
            inventoryController = CreateController(placeForUi, model);
        }

        public InventoryController CreateController(Transform placeForUi, IInventoryModel model)
        {
            IInventoryView view = LoadInventoryView(placeForUi);
            IItemsRepository repository = CreateInventoryRepository();

            var inventoryController = new InventoryController(view, model, repository);
            AddController(inventoryController);
            return inventoryController;
        }

        private IInventoryView LoadInventoryView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewInventoryPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<IInventoryView>();
        }

        private IItemsRepository CreateInventoryRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourceInventoryPath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }
    }
}