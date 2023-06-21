using Features.AbilitySystem;
using Features.Shed;
using UnityEngine;

namespace CarGame
{
    class MainController : BaseController
    {
        private readonly Transform _placeForUI;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private SettingsController _settingsController;

        private ShedContext _shedContext;


        public MainController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _placeForUI = placeForUI;
            _profilePlayer = profilePlayer;

            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            DisposeControllers();

            _profilePlayer.CurrentState.UnsubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();

            switch (state)
            {
                case (GameState.Game):
                    UnityAdsService.instance.InterstitialPlayer.Play();
                    _gameController = new GameController(_placeForUI, _profilePlayer);
                    break;

                case (GameState.Start):
                    _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                    break;

                case (GameState.Settings):
                    _settingsController = new SettingsController(_placeForUI, _profilePlayer);
                    break;

                case (GameState.Shed):
                    _shedContext = new ShedContext(_placeForUI, _profilePlayer);
                    break;

                default:
                    Debug.LogWarning("No controller for this state.");
                    break;
            }
        }

        private void DisposeControllers()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _settingsController?.Dispose();
            _shedContext?.Dispose();
        }
    }
}