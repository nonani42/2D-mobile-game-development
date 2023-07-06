using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    internal class BackToMenuView : MonoBehaviour
    {
        [SerializeField] private Button _backToMenuButton;

        private UnityAction _backToMenu;

        public void Init(UnityAction backToMenu)
        {
            _backToMenu = backToMenu;
            _backToMenuButton.onClick.AddListener(_backToMenu);
        }

        private void OnDestroy() =>
            _backToMenuButton.onClick.RemoveListener(_backToMenu);
    }
}
