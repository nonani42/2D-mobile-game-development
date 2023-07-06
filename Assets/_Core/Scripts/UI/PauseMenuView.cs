using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backToMainMenuButton;

        private UnityAction _continue;
        private UnityAction _backToMainMenu;

        public void Init(UnityAction continueGame, UnityAction backToMainMenu)
        {
            _continue = continueGame;
            _backToMainMenu = backToMainMenu;

            _continueButton.onClick.AddListener(_continue);
            _backToMainMenuButton.onClick.AddListener(_backToMainMenu);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(_continue);
            _backToMainMenuButton.onClick.RemoveListener(_backToMainMenu);
        }
    }
}