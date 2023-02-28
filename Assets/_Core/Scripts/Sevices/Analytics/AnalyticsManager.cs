using UnityEngine;

namespace CarGame
{
    internal class AnalyticsManager : MonoBehaviour
    {
        public static AnalyticsManager instance { get; private set; }

        private IAnalyticsService[] _services;


        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendLevelStarted() =>
            SendEvent("LevelStarted");


        private void SendEvent(string eventName)
        {
            Debug.Log($"{eventName}");
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }

        public void Transaction(UnityEngine.Purchasing.Product product)
        {
            Debug.Log($"Transaction: {product.definition.storeSpecificId}, {product.metadata.localizedPrice}, {product.metadata.isoCurrencyCode}"); 
            for (int i = 0; i < _services.Length; i++)
                _services[i].Transaction(product);
        }
    }
}
