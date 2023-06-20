using UnityEngine;

namespace CarGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUI;

        [Header("Initial Settings")]
        [SerializeField] private float _speedCar;
        [SerializeField] private float _jumpHeightCar;
        [SerializeField] private GameState InitialState;

        private MainController _mainController;

        private void Start()
        {
            ProfilePlayer profilePlayer = new ProfilePlayer(_speedCar, _jumpHeightCar, InitialState);
            _mainController = new MainController(_placeForUI, profilePlayer);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
