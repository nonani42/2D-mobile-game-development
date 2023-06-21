using Tool;
using UnityEngine;

namespace CarGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUI;

        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/InitialSettingsConfig");
        private InitialSettingsConfig _initialSettingsConfig;

        private float _speedCar;
        private float _jumpHeightCar;
        private GameState InitialState;

        private MainController _mainController;


        private void Start()
        {
            ProfilePlayer profilePlayer = CreateProfilePlayer();

            _mainController = new MainController(_placeForUI, profilePlayer);
        }

        private ProfilePlayer CreateProfilePlayer()
        {
            _initialSettingsConfig = ContentDataSourceLoader.LoadInitialSettingsConfig(_dataSourcePath);

            _speedCar = _initialSettingsConfig.Car.SpeedCar;
            _jumpHeightCar = _initialSettingsConfig.Car.JumpHeightCar;
            InitialState = _initialSettingsConfig.InitialState;

            return new ProfilePlayer(_speedCar, _jumpHeightCar, InitialState);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
