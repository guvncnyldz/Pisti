using UnityEngine;
//using GoogleMobileAds.Api;
using System;

public class AdManagerBanner
{
    private bool isBannerLoaded;
    private bool isBannerShowable;
    private bool isInitilizationCompleted;
    public AdManagerBanner()
    {
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;

        isBannerShowable = true;
    }

    public void OnInitializationCompleted()
    {
        isInitilizationCompleted = true;
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    private void BannerOnAdLoadedEvent(IronSourceAdInfo ınfo)
    {
        isBannerLoaded = true;
        DisplayBanner();
    }

    void DisplayBanner()
    {
        if (!isBannerLoaded || !isBannerShowable || !isInitilizationCompleted)
            return;

        IronSource.Agent.displayBanner();
    }

    public void ActivateBanner(bool isActive)
    {
        isBannerShowable = isActive;

        if (isActive)
            DisplayBanner();
        else
            IronSource.Agent.hideBanner();
    }
}
