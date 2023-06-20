using Features.AbilitySystem;
using UnityEngine;

namespace CarGame
{
    internal class GameController : BaseController
    {
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

            carController = new CarController(profilePlayer.CurrentCar);
            inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            abilitiesController = new AbilitiesController(placeForUi, carController);

            AddController(tapeBackgroundController);
            AddController(inputGameController);
            AddController(carController);
            AddController(abilitiesController);

            AnalyticsManager.instance.SendLevelStarted();
        }
    }
}