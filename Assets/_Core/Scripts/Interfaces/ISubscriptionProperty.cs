using System;

public interface ISubscriptionProperty<TValue>
{
    TValue Value { get; }

    void SubscribeOnChange(Action<TValue> subscriptinAction);
    void UnsubscribeOnChange(Action<TValue> subscriptinAction);
}
