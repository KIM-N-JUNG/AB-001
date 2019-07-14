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

    private const string APP_ID = "ca-app-pub-1339724987571025~1648266314";

    private const string ANDROID_BANNER_ID = "ca-app-pub-1339724987571025/9830363880";

    bool bAdsLoaded = false;


    public void Start()
    {
        initAds();

        // RequestBannerAd();
        // RequestInterstitialAd();

        // ShowBannerAd();
    }

    public void initAds()
    {
        Debug.Log("AdmobManager initAds");
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
        Debug.Log("RequestBannerAd");
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = ANDROID_BANNER_ID;
#elif UNITY_IOS
        adUnitId = ios_bannerAdUnitId;
#endif

        // bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom);
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    private void RequestInterstitialAd()
    {
        Debug.Log("RequestInterstitialAd");
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
        Debug.Log("HandleOnInterstitialAdClosed event received.");

        interstitialAd.Destroy();

        RequestInterstitialAd();
    }

    public void ShowBannerAd()
    {
        Debug.Log("ShowBannerAd");
        bannerView.Show();
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("ShowInterstitialAd");
        if (!interstitialAd.IsLoaded())
        {
            RequestInterstitialAd();
            return;
        }

        interstitialAd.Show();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("HandleOnAdLoaded");
        bAdsLoaded = true;
    }

    public Boolean IsLoaded()
    {
        return bAdsLoaded;
    }
}