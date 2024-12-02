using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLevelComplete : EventBase
{
    public override string eventName => "level_complete";

    public EventLevelComplete(string result) : base()
    {
        parameters.Add("level_index", GameData.PlayCounter.ToString());
        parameters.Add("result", result);
        GameData.PlayCounter++;
    }
}
