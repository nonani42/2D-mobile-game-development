using Profile;
using Tool;
using UnityEngine;

namespace UI
{
    internal class PauseMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/PauseMenuView");

        private readonly PauseMenuView _view;
        private readonly PlayerProfile _profilePlayer;
        private readonly Pause _pause;


        public PauseMenuController(Transform placeForUi, PlayerProfile profilePlayer, Pause pause)
        {
            _profilePlayer = profilePlayer;

            _pause = pause;
            Subscribe(_pause);

            _view = LoadView(placeForUi);
            _view.Init(ContinueGame, BackToMainMenu);

            if (_pause.IsEnabled)
                _view.Show();
            else
                _view.Hide();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Unsubscribe(_pause);
        }

        private void Subscribe(Pause pause)
        {
            pause.Enabled += OnPauseEnabled;
            pause.Disabled += OnPauseDisabled;
        }

        private void Unsubscribe(Pause pause)
        {
            pause.Enabled -= OnPauseEnabled;
            pause.Disabled -= OnPauseDisabled;
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void ContinueGame() => _pause.Disable();
        private void BackToMainMenu() => _profilePlayer.CurrentState.Value = GameState.Start; 

        private void OnPauseEnabled() => _view.Show();
        private void OnPauseDisabled() => _view.Hide();
    }
}