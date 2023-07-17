using Profile;
using Tool;
using Tool.Notifications;
using Tool.PushNotifications;
using Tool.PushNotifications.Settings;
using UnityEngine;

namespace CarGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUI;

        private readonly ResourcePath _soundManagerPrefabPath = new ResourcePath("Prefabs/SoundManager");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/InitialSettingsConfig");
        private InitialSettingsConfig _initialSettingsConfig;

        [Header("Settings")]
        [SerializeField] private NotificationSettings _settings;

        private float _speedCar;
        private float _jumpHeightCar;
        private GameState InitialState;

        private MainController _mainController;
        private NotificationController _notificationController;


        private void Start()
        {
            CreateSoundManager();
            PlayerProfile playerProfile = CreateProfilePlayer();

            _mainController = new MainController(_placeForUI, playerProfile);

            _notificationController = new NotificationController(playerProfile, _settings);
        }

        private void CreateSoundManager()
        {
            var temp = ResourcesLoader.LoadObject<SoundManager>(_soundManagerPrefabPath);
            Instantiate(temp, null);
        }

        private PlayerProfile CreateProfilePlayer()
        {
            _initialSettingsConfig = ContentDataSourceLoader.LoadInitialSettingsConfig(_dataSourcePath);

            _speedCar = _initialSettingsConfig.Car.SpeedCar;
            _jumpHeightCar = _initialSettingsConfig.Car.JumpHeightCar;
            InitialState = _initialSettingsConfig.InitialState;

            return new PlayerProfile(_speedCar, _jumpHeightCar, InitialState);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
            _notificationController.Dispose();
        }
    }
}
