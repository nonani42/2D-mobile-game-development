using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    class BackToMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/BackToMenuView");

        private readonly BackToMenuView _view;
        private readonly PlayerProfile _profilePlayer;


        public BackToMenuController(Transform placeForUi, PlayerProfile profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(BackToMenu);
        }

        private BackToMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BackToMenuView>();
        }

        private void BackToMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
