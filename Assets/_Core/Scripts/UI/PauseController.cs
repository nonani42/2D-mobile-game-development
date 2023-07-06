using Profile;
using Tool;
using UnityEngine;

namespace UI
{
    internal class PauseController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/PauseButtonView");

        private readonly PauseView _view;
        private readonly ProfilePlayer _profilePlayer;


        public PauseController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(ToPauseMenu);
        }

        private PauseView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseView>();
        }

        private void ToPauseMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Pause;
    }
}