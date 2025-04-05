using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEventSO<T> : ScriptableObject
{
    [TextArea]
    public string description;
    
    public UnityAction<T> OnEventRaised;
    public string lastSender;
    
    public void RaiseEvent(T value,object sender)
    {
        lastSender = sender.ToString();
        OnEventRaised?.Invoke(value);

    }
}
