using Profile;
using Tool;
using UnityEngine;

namespace UI
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/PauseMenuView");

        private readonly PauseMenuView _view;
        private readonly ProfilePlayer _profilePlayer;


        public PauseMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(ContinueGame, BackToMainMenu);
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void ContinueGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void BackToMainMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}