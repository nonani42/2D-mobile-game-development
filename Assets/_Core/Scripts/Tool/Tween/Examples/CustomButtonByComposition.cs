using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByComposition : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _rectTransform;

        [Header("Settings")]
        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private float _jumpStrength = 0.5f;
        [SerializeField] private int _jumpCount = 5;
        [SerializeField] private bool _isIndependentUpdate = true;

        private Sequence _sequence;
        private Tweener _tweenAnimation;


        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() { _button.onClick.RemoveAllListeners(); }

        private void InitComponents()
        {
            if(_button == null)
                _button = GetComponent<Button>();

            if(_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
        }


        private void OnButtonClick() =>
            ActivateAnimation();

        [ContextMenu(nameof(ActivateAnimation))]
        private void ActivateAnimation()
        {
            StopAnimation();
            switch (_animationButtonType)
            {
                case AnimationButtonType.ChangeRotation:
                    _tweenAnimation = _rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase).SetUpdate(_isIndependentUpdate);
                    break;

                case AnimationButtonType.ChangePosition:
                    _tweenAnimation = _rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase).SetUpdate(_isIndependentUpdate);
                    break;
            }
        }

        [ContextMenu(nameof(StopAnimation))]
        public void StopAnimation()
        {
            _tweenAnimation?.Kill();
        }

        [ContextMenu(nameof(PlaySequence))]
        public void PlaySequence()
        {
            StopSequence();
            _sequence = DOTween.Sequence();

            _sequence.Append(_rectTransform.DOJump(Vector2.up, _jumpStrength, _jumpCount, _duration));
            _sequence.Append(_rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase));
            _sequence.Append(_rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase));
        }

        [ContextMenu(nameof(StopSequence))]
        public void StopSequence()
        {
            _sequence?.Kill();
        }
    }
}
