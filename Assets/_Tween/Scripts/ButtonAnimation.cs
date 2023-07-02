using DG.Tweening;
using System.Linq;
using UnityEngine;

namespace Tween
{
    class ButtonAnimation
    {
        private RectTransform _rectTransform;

        private Sequence sequence;

        private Ease _curveEase = Ease.Linear;

        private float _duration = 2f;
        private float _jumpStrength = 0.5f;
        private float _strength = 30f;
        private int _jumpCount = 5;

        public ButtonAnimation(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;

        }

        public void PlaySequence()
        {
            sequence = DOTween.Sequence();

            sequence.Append(_rectTransform.DOJump(Vector2.up, _jumpStrength, _jumpCount, _duration));
            sequence.Append(_rectTransform.DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase));
            sequence.Append(_rectTransform.DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase));
        }

        public void StopSequence()
        {
            sequence.Kill();
        }
    }
}
