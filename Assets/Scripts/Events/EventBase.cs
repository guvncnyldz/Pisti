using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventBase
{
    public abstract string eventName { get; }
    public Dictionary<string, string> parameters {get; protected set;}

    public EventBase()
    {

    }

    public void SendEvent()
    {
        EventManager.SendEvent(this);
    }
}
