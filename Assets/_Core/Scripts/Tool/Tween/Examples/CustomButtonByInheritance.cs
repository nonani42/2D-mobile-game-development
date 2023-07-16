using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tween
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomButtonByInheritance : Button
    {
        public static string AnimationTypeName => nameof(_animationButtonType);
        public static string CurveEaseName => nameof(_curveEase);
        public static string DurationName => nameof(_duration);
        public static string JumpStrength => nameof(_jumpStrength);
        public static string JumpCount => nameof(_jumpCount);
        public static string IsIndependentUpdate => nameof(_isIndependentUpdate);

        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private AnimationButtonType _animationButtonType = AnimationButtonType.ChangePosition;
        [SerializeField] private Ease _curveEase = Ease.Linear;
        [SerializeField] private float _duration = 0.6f;
        [SerializeField] private float _strength = 30f;
        [SerializeField] private float _jumpStrength = 0.5f;
        [SerializeField] private int _jumpCount = 5;
        [SerializeField] private bool _isIndependentUpdate = true;

        private Sequence _sequence;
        private Tweener _tweenAnimation;

        protected override void Awake()
        {
            base.Awake();
            InitRectTransform();
        }

        //protected override void OnValidate()
        //{
        //    base.OnValidate();
        //    InitRectTransform();
        //}

        private void InitRectTransform()
        {
            if(_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ActivateAnimation();
        }

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
