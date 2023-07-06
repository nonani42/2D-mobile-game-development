using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private Button _pauseMenuButton;

        private UnityAction _toPauseMenu;

        public void Init(UnityAction pauseMenu)
        {
            _toPauseMenu = pauseMenu;
            _pauseMenuButton.onClick.AddListener(_toPauseMenu);
        }

        private void OnDestroy() =>
            _pauseMenuButton.onClick.RemoveListener(_toPauseMenu);
    }
}