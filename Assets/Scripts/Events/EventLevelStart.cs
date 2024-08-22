using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevelStart : EventBase
{
    public override string eventName => "level_start";

    public EventLevelStart() : base()
    {
        parameters.Add("level_index",GameData.PlayCounter.ToString());
    } 
}
