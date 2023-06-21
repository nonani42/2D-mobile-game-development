using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace CarGame
{
    internal class GameController : BaseController
    {
        private readonly ResourcePath _viewAbilityPath = new ResourcePath("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourceAbilityPath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");

        SubscriptionProperty<float> leftMoveDiff;
        SubscriptionProperty<float> rightMoveDiff;

        CarController carController;
        InputGameController inputGameController;
        TapeBackgroundController tapeBackgroundController;
        AbilitiesController abilitiesController;

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            leftMoveDiff = new SubscriptionProperty<float>();
            rightMoveDiff = new SubscriptionProperty<float>();

            IAbilitiesView abilityView = LoadAbilitiesView(placeForUi);
            IEnumerable<IAbilityItem> abilityItems = LoadAbilityItemConfigs();
            IAbilitiesRepository abilityRepository = CreateAbilitiesRepository(abilityItems);

            carController = new CarController(profilePlayer.CurrentCar);
            inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            abilitiesController = new AbilitiesController(abilityView, abilityRepository, carController, abilityItems);

            AddController(tapeBackgroundController);
            AddController(inputGameController);
            AddController(carController);
            AddController(abilitiesController);

            AnalyticsManager.instance.SendLevelStarted();
        }

        private IEnumerable<IAbilityItem> LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourceAbilityPath);

        private IAbilitiesRepository CreateAbilitiesRepository(IEnumerable<IAbilityItem> abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private IAbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewAbilityPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}