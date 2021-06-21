using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManagerBanner : MonoBehaviour
{
    public static AdManagerBanner instance;

    private BannerView bannerView;

    // string App_ID = "ca-app-pub-2366580648935894~4263881781";

    public string androidId = "ca-app-pub-2366580648935894/2232097896";
    // public string androidIdorigi = "ca-app-pub-2366580648935894/2232097896";
    public string iosId = "ca-app-pub-2366580648935894/3919047607";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // MobileAds.Initialize(App_ID);

        this.RequestBanner();
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = androidId;
#elif UNITY_IPHONE
            string adUnitId = iosId;
#else
            string adUnitId = "unexpected_platform";
#endif


        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        ShowBanner();
    }

    private void ShowBanner()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    public void HideBanner()
    {
        this.bannerView.Destroy();
    }


    // EVENTS AND DELEGATES

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestBanner();
        Debug.LogError("WTF");
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.LogError("HELALAAAAAAAAAAAAAAAAAAAAAAAL");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
        Debug.LogError("HELAAAL");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


}
