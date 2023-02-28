using System.Collections.Generic;

namespace CarGame
{
    internal interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
        void Transaction(UnityEngine.Purchasing.Product product);
    }
}
