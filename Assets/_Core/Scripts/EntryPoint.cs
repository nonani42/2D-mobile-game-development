using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUI;

        private const float SpeedCar = 15f;
        private const GameState InitialState = GameState.Start;

        private MainController _mainController;

        private void Start()
        {
            ProfilePlayer profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
            _mainController = new MainController(_placeForUI, profilePlayer);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}
