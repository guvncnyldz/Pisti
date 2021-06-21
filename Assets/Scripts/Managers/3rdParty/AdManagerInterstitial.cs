using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManagerInterstitial : MonoBehaviour
{
    public static AdManagerInterstitial instance;

    public InterstitialAd interstitial;

    // string App_ID = "ca-app-pub-2366580648935894~4263881781";

    public string androidId = "ca-app-pub-2366580648935894/1637718448";
    // public string androidIdorigi = "ca-app-pub-2366580648935894/1637718448";
    public string iosId = "ca-app-pub-2366580648935894/8979802595";

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
        //RequestInterstitial();
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = androidId;
#elif UNITY_IPHONE
        string adUnitId = iosId;
#else
        string adUnitId = "unexpected_platform";
#endif


        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAD()
    {

        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }




    // EVENTS AND DELEGATES

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.LogError("HELALAAAAAAAAAAAAAAAAAAAAAAAL");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
        Debug.LogError("WTF");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
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
