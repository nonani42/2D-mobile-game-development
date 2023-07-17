using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CarGame
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/MainMenu");
        private readonly PlayerProfile _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUI, PlayerProfile profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUI);
            _view.Init(StartGame, GoToSettings, WatchRewardedAd, Buy, GoToShed, OpenDailyReward, Exit);

            AnalyticsManager.instance.SendMainMenuOpened();
        }

        private MainMenuView LoadView(Transform placeForUI)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            AddGameObject(objectView);
            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() => _profilePlayer.CurrentState.Value = GameState.Game;
        private void GoToSettings() => _profilePlayer.CurrentState.Value = GameState.Settings;
        private void WatchRewardedAd() => UnityAdsService.instance.RewardedPlayer.Play();
        private void Buy(string productId) => IAPService.instance.Buy(productId);
        private void GoToShed() => _profilePlayer.CurrentState.Value = GameState.Shed;
        private void OpenDailyReward() => _profilePlayer.CurrentState.Value = GameState.DailyReward;
        
        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}