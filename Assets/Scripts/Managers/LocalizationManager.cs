using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private static LocalizationManager Instance;
    public List<LocalizationDataSO> localizationDataSOList;

    public void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SystemLanguage language = Application.systemLanguage;
        LocalizationUtil.currentLocalizationData = GetLanguageData(language);
    }

    public LocalizationDataSO GetLanguageData(SystemLanguage systemLanguage)
    {
        LocalizationDataSO localizationDataSO = localizationDataSOList.FirstOrDefault(localiztionData => localiztionData.systemLanguage == systemLanguage);

        if (localizationDataSO == null)
        {
            return localizationDataSOList.FirstOrDefault(localiztionData => localiztionData.systemLanguage == SystemLanguage.English);
        }

        return localizationDataSO;
    }
}

public static class LocalizationUtil
{
    public static LocalizationDataSO currentLocalizationData;
    public static void SetLocalizedText(this TextMeshProUGUI textMeshPro, string key)
    {
        textMeshPro.text = string.Format(currentLocalizationData.GetLocalizedText(key));
    }

    public static void SetLocalizedText(this TextMeshProUGUI textMeshPro, string key, params string[] parameters)
    {
        textMeshPro.text = string.Format(currentLocalizationData.GetLocalizedText(key), parameters);
    }

    public static string GetLocalizedText(string key)
    {
        return string.Format(currentLocalizationData.GetLocalizedText(key));
    }

    public static string GetLocalizedText(string key, params string[] parameters)
    {
        return string.Format(currentLocalizationData.GetLocalizedText(key), parameters);
    }
}