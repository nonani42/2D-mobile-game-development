﻿using Features.AbilitySystem;
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
        private ShedController _shedController;


        public MainController(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _placeForUI = placeForUI;
            _profilePlayer = profilePlayer;

            _profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _settingsController?.Dispose();

            _profilePlayer.CurrentState.UnsubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case (GameState.Game):
                    UnityAdsService.instance.InterstitialPlayer.Play();
                    _gameController = new GameController(_placeForUI, _profilePlayer);
                    _mainMenuController?.Dispose();
                    _settingsController?.Dispose();
                    _shedController?.Dispose();
                    break;
                case (GameState.Start):
                    _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                    _gameController?.Dispose();
                    _settingsController?.Dispose();
                    _shedController?.Dispose();
                    break;
                case (GameState.Settings):
                    _settingsController = new SettingsController(_placeForUI, _profilePlayer);
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    _shedController?.Dispose();
                    break;
                case (GameState.Shed):
                    _shedController = new ShedController(_placeForUI, _profilePlayer);
                    _mainMenuController?.Dispose();
                    _gameController?.Dispose();
                    _settingsController?.Dispose();
                    break;

                default:
                    _gameController?.Dispose();
                    _mainMenuController?.Dispose();
                    _settingsController?.Dispose();
                    _shedController?.Dispose();
                    Debug.Log("No controller for this state.");
                    break;
            }
        }
    }
}