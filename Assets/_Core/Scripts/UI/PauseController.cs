using Profile;
using Tool;
using UnityEngine;

namespace UI
{
    internal class PauseController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/PauseButtonView");

        private readonly PauseView _view;
        private readonly PlayerProfile _profilePlayer;
        private readonly Transform _placeForUi;
        private readonly Pause _pause;

        private PauseMenuController _pauseMenuController;


        public PauseController(Transform placeForUi, PlayerProfile profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;

            _view = LoadView(placeForUi);
            _view.Init(ToPauseMenu);

            _pause = new Pause();

            _pauseMenuController = CreatePauseMenuController(_placeForUi, _profilePlayer, _pause);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            if (_pause.IsEnabled)
                _pause.Disable();
        }

        private void ToPauseMenu()
        {
            if (!_pause.IsEnabled)
                _pause.Enable();
        }

        private PauseView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseView>();
        }

        private PauseMenuController CreatePauseMenuController(Transform placeForUi, PlayerProfile profilePlayer, Pause pause)
        {
            var pauseMenuController = new PauseMenuController(placeForUi, profilePlayer, pause);
            AddController(pauseMenuController);

            return pauseMenuController;
        }
    }
}