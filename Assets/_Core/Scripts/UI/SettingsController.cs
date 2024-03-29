﻿using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame
{
    internal class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/SettingsMenu");
        private readonly PlayerProfile _profilePlayer;
        private readonly SettingsView _view;

        public SettingsController(Transform placeForUI, PlayerProfile profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.Init(GoBackToMenu);
        }

        private SettingsView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);
            return objectView.GetComponent<SettingsView>();
        }

        private void GoBackToMenu() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}