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
            _initialSettingsConfig = ContentDataSourceLoader.LoadInitialSettingsConfig(_dataSourcePath);

            _speedCar = _initialSettingsConfig.SpeedCar;
            _jumpHeightCar = _initialSettingsConfig.JumpHeightCar;
            InitialState = _initialSettingsConfig.InitialState;

            ProfilePlayer profilePlayer = new ProfilePlayer(_speedCar, _jumpHeightCar, InitialState);
            _mainController = new MainController(_placeForUI, profilePlayer);
        }


        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
