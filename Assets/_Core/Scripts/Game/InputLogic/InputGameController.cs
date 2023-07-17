using Tool;
using UnityEngine;

namespace CarGame
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Input/EndlessMove");

        private readonly BaseInputView _view;

        public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, CarModel car)
        {
#if UNITY_EDITOR
            _resourcePath = new ResourcePath("Prefabs/Input/KeyboardMove");
#endif

            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);
        }

        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);
            return objectView.GetComponent<BaseInputView>();
        }
    }
}