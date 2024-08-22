using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

public static class EventManager
{
    public static void SendEvent(EventBase eventBase)
    {
        string eventName = eventBase.eventName;
        Dictionary<string, string> parameters = eventBase.parameters;

#if UNITY_EDITOR
        string parameterLog = $"[{eventName}]";
        foreach (KeyValuePair<string, string> parameter in parameters)
        {
            parameterLog += $"{parameter.Key}-{parameter.Value}\n";
        }

        Debug.Log($"<color=green>{parameterLog}</color>");
#endif
        Parameter[] firebaseParameters = new Parameter[parameters.Count];
        int parameterIndex = 0;

        foreach (KeyValuePair<string, string> parameter in parameters)
        {
            firebaseParameters[parameterIndex] = new Parameter(parameter.Key, parameter.Value);
            parameterIndex++;
        }

        FirebaseAnalytics.LogEvent(eventName, firebaseParameters);
    }
}
