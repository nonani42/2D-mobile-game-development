using Features.Battle;
using Features.Rewards;
using Features.Shed;
using Profile;
using UI;
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
        private RewardsController _rewardsController;
        private BattleController _battleController;
        private BackToMenuController _backToMenuController; 
        private PauseMenuController _pauseMenuController;


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
            DisposeChildObjects();

            _profilePlayer.CurrentState.UnsubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeChildObjects();

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

                case (GameState.DailyReward):
                    _rewardsController = new RewardsController(_placeForUI, _profilePlayer);
                    break;

                case (GameState.Battle):
                    _battleController = new BattleController(_placeForUI, _profilePlayer);
                    break;

                case (GameState.Pause):
                    _pauseMenuController = new PauseMenuController(_placeForUI, _profilePlayer);
                    break;

                default:
                    Debug.LogWarning("No controller for this state.");
                    break;
            }

            if (state != GameState.Start && state != GameState.Pause)
                _backToMenuController = new BackToMenuController(_placeForUI, _profilePlayer);
        }

        private void DisposeChildObjects()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _settingsController?.Dispose();
            _shedContext?.Dispose();
            _rewardsController?.Dispose();
            _battleController?.Dispose();
            _backToMenuController?.Dispose();
            _pauseMenuController?.Dispose();
        }
    }
}