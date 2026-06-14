using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public abstract class GenericEventChannelSO<T> : ScriptableObject
{
    public Action<T> onEventRaised;
    public void raiseEvent(T value)
    {
        onEventRaised?.Invoke(value);
    }
}

