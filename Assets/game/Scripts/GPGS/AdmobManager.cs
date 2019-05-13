using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public string android_banner_id;
    //public string ios_banner_id;

    public string android_interstitial_id;
    //public string ios_interstitial_id;

    private BannerView bannerView;
    private InterstitialAd interstitialAd;

    private const string APP_ID = "ca-app-pub-1339724987571025~2941971617";




    public void Start()
    {
        initAds();

        RequestBannerAd();
        RequestInterstitialAd();

        ShowBannerAd();
    }

    public void initAds()
    {
#if UNITY_ANDROID
        string appId = APP_ID;
#elif UNITY_IPHONE
            //string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            //string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
    }

    public void RequestBannerAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_banner_id;
#elif UNITY_IOS
        adUnitId = ios_bannerAdUnitId;
#endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd()
    {
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = android_interstitial_id;
#elif UNITY_IOS
        adUnitId = ios_interstitialAdUnitId;
#endif

        interstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();

        interstitialAd.LoadAd(request);

        interstitialAd.OnAdClosed += HandleOnInterstitialAdClosed;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        print("HandleOnInterstitialAdClosed event received.");

        interstitialAd.Destroy();

        RequestInterstitialAd();
    }

    public void ShowBannerAd()
    {
        bannerView.Show();
    }

    public void ShowInterstitialAd()
    {
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }

        interstitialAd.Show();
    }

}