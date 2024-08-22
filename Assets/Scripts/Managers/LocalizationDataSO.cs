using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Localiztion Data", menuName = "ScriptableObjects/Localiztion/Create Localization Data")]
public class LocalizationDataSO : ScriptableObject
{
    public SystemLanguage systemLanguage;
    [SerializeField] private List<LocalizationPair> localizationPairs;

    public string GetLocalizedText(string key)
    {
        LocalizationPair localizationPair = localizationPairs.FirstOrDefault(pair => pair.Key == key);

        return localizationPair == null ? key : localizationPair.Value;
    }
}

[Serializable]
public class LocalizationPair
{
    public string Key;
    [TextArea]
    public string Value;
}