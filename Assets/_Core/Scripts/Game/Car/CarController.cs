using Features.AbilitySystem;
using Tool;
using UnityEngine;

namespace CarGame
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Car");
        private readonly CarView _view;
        private readonly CarModel _model;


        GameObject IAbilityActivator.ViewGameObject => _view.gameObject;
        float IAbilityActivator.JumpHeight => _model.JumpHeight;

        public CarController(CarModel model)
        {
            _model = model;
            _view = LoadView();
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);
            return objectView.GetComponent<CarView>();
        }
    }
}