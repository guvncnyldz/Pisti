using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLocalizationHelper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string key;

    void Start()
    {
        text.SetLocalizedText(key);
    }
}
