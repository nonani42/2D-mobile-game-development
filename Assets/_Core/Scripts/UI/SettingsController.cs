using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame
{
    internal class SettingsController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Settings");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsView _view;

        public SettingsController(Transform placeForUI, ProfilePlayer profilePlayer)
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