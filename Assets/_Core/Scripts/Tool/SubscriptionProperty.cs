using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriptionProperty<TValue> : ISubscriptionProperty<TValue>
{
    private TValue _value;
    private Action<TValue> _onChangeValue;

    public TValue Value
    {
        get => _value;
        set
        {
            _value = value;
            _onChangeValue?.Invoke(_value);
        }
    }

    public void SubscribeOnChange(Action<TValue> subscriptinAction) => _onChangeValue += subscriptinAction;
    public void UnsubscribeOnChange(Action<TValue> subscriptinAction) => _onChangeValue -= subscriptinAction;
}
