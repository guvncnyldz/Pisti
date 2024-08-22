using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevelComplete : EventBase
{
    public override string eventName => "level_complete";

    public EventLevelComplete() : base()
    {
        parameters.Add("level_index",GameData.PlayCounter.ToString());
        GameData.PlayCounter++;
    } 
}
