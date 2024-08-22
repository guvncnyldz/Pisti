using UnityEngine;
//using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class AdManagerInterstitial
{
    private UnityAction onClosed, onFailed;
    private bool isInitilizationCompleted;

    public AdManagerInterstitial()
    {
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
    }

    public void OnInitializationCompleted()
    {
        isInitilizationCompleted = true;
        IronSource.Agent.loadInterstitial();
    }

    public void ShowInterstitialAD(string placementName, UnityAction onClosed, UnityAction onFailed)
    {
        if (!isInitilizationCompleted)
        {
            onClosed?.Invoke();
            return;
        };

        if (IronSource.Agent.isInterstitialReady())
        {
            this.onClosed = onClosed;
            this.onFailed = onFailed;

            IronSource.Agent.showInterstitial();
        }
        else
        {
            onFailed?.Invoke();
            IronSource.Agent.loadInterstitial();
        }
    }

    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        onFailed?.Invoke();
    }
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        IronSource.Agent.loadInterstitial();
        onClosed?.Invoke();
    }
}
