using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CarGame
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    class ButtonAnimation : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;


        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() { _button.onClick.RemoveAllListeners(); }

        private void InitComponents()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

        }

        private void OnButtonClick() =>
            ActivateAnimation();

        private void ActivateAnimation()
        {
                _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
        }
    }
}
