using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubscriptionProperty<TValue>
{
    TValue Value { get; }

    void SubscribeOnChange(Action<TValue> subscriptinAction);
    void UnsubscribeOnChange(Action<TValue> subscriptinAction);
}
