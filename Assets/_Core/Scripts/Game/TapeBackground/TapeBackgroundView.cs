using System;
using UnityEngine;

namespace CarGame
{
    public class TapeBackgroundView : MonoBehaviour
    {
        [SerializeField] private Background[] _backgrounds;

        private ISubscriptionProperty<float> _diff;

        public void Init(ISubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
        }

        private void OnDestroy()
        {
            _diff.UnsubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            foreach (Background background in _backgrounds)
            {
                background.Move(-value);
            }
        }
    }
}