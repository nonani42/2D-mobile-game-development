﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CarGame
{
    class MainController : BaseController
    {
        private readonly Transform _placeForUI;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private GameController _gameController;

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

            _profilePlayer.CurrentState.UnsubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case (GameState.Game):
                    _gameController = new GameController(_profilePlayer);
                    _mainMenuController?.Dispose();
                    break;
                case (GameState.Start):
                    _mainMenuController = new MainMenuController(_placeForUI, _profilePlayer);
                    _gameController?.Dispose();
                    break;
                default:
                    _gameController?.Dispose();
                    _mainMenuController?.Dispose();
                    Debug.Log("No controller for this state.");
                    break;
            }
        }
    }
}