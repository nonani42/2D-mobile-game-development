using System.Collections.Generic;
using UnityEngine.Analytics;

namespace CarGame
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName) =>
            Analytics.CustomEvent(eventName);

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            Analytics.CustomEvent(eventName, eventData);

        public void Transaction(UnityEngine.Purchasing.Product product) =>
            Analytics.Transaction(product.definition.storeSpecificId, product.metadata.localizedPrice, product.metadata.isoCurrencyCode);
    }
}
