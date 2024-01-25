using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/String Event")]
public class StringEvent : ScriptableObjectBase
{
    public UnityAction<string> onEventRaised;

    public void RaisedEvent(string value)
    {
        onEventRaised?.Invoke(value); //? null check
    }

    public void Subscribe(UnityAction<string> function)
    {
        onEventRaised += function;
    }

    public void Unsubscribe(UnityAction<string> function)
    {
        onEventRaised -= function;
    }
}

