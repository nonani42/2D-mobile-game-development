using UnityEngine;

namespace CarGame
{
    public class TapeBackgroundController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Background");

        private readonly SubscriptionProperty<float> _diff;
        private readonly ISubscriptionProperty<float> _leftMoveDiff;
        private readonly ISubscriptionProperty<float> _rightMoveDiff;

        private  TapeBackgroundView _view;


        public TapeBackgroundController(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            _view = LoadView();
            _diff = new SubscriptionProperty<float>();

            _leftMoveDiff = leftMoveDiff;
            _rightMoveDiff = rightMoveDiff;

            _view.Init(_diff);

            _leftMoveDiff.SubscribeOnChange(MoveLeft);
            _rightMoveDiff.SubscribeOnChange(MoveRight);
        }

        private TapeBackgroundView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);
            return objectView.GetComponent<TapeBackgroundView>();
        }

        protected override void OnDispose()
        {
            _leftMoveDiff.UnsubscribeOnChange(MoveLeft);
            _rightMoveDiff.UnsubscribeOnChange(MoveRight);
        }

        private void MoveRight(float value) => _diff.Value = value;

        private void MoveLeft(float value) => _diff.Value = -value;
    }
}