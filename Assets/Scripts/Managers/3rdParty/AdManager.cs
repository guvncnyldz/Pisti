using System.Collections;
using System.Collections.Generic;
using com.unity3d.mediation;
using UnityEngine;

public class AdManager : MonoBehaviour
{
#if UNITY_ANDROID
    private string addKey = "1f66966e5";
#else
    private string addKey = "1f66966e5";
#endif
    public static AdManager Instance;

    public AdManagerBanner adManagerBanner;
    public AdManagerInterstitial adManagerInterstitial;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        IronSource.Agent.validateIntegration();

        //IronSource.Agent.setMetaData("is_test_suite", "enable");

        IronSource.Agent.init(addKey, IronSourceAdUnits.INTERSTITIAL, LevelPlayAdFormat.BANNER.ToString());

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        adManagerBanner = new AdManagerBanner();
        adManagerInterstitial = new AdManagerInterstitial();

        DontDestroyOnLoad(gameObject);
    }

    private void SdkInitializationCompletedEvent()
    {
        adManagerInterstitial.OnInitializationCompleted();
        adManagerBanner.OnInitializationCompleted();

        //IronSource.Agent.launchTestSuite();
    }
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
