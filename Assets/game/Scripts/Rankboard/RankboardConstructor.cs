
using System;
using System.Collections;
using System.Collections.Generic;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using Ab001.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class RankboardConstructor : MonoBehaviour
{
    public GameObject ui;

    // for AdMob
    private BannerView bannerView;

    private const string APP_ID = "ca-app-pub-1339724987571025~1648266314";

    private const string BANNER_ID = "ca-app-pub-1339724987571025/1266240866";


    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("RankboardConstructor Awake");
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.RANK_BOARD)
        {
            initBanner();
            ui.SetActive(true);
        }
    }

    void Update()
    {
#if UNITY_ANDROID
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.RANK_BOARD)
        {
            //if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    Quit();

                    SceneManager.LoadScene((int)Constant.SceneNumber.MAIN_MENU);
                    return;
                }
            }
        }
#endif
    }

    void Start()
    {
        Debug.Log("RankboardConstructor Start");
    }

    public void Quit()
    {
        bannerView.Hide();
        Debug.Log("Quit");
    }

    private void initBanner()
    {
        Debug.Log("initBanner");
        string adUnitId = string.Empty;

#if UNITY_ANDROID
        adUnitId = BANNER_ID;
#elif UNITY_IOS
        // adUnitId = ios_bannerAdUnitId;
#endif
        Debug.Log("initBanner adUnitId : " + adUnitId);

        // bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Top);
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleOnAdLoaded;

        AdRequest request = new AdRequest.Builder().Build();
        Debug.Log("bannerView.LoadAd()");
        bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.RANK_BOARD)
        {
            return;
        }
        bannerView.Show();
        MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("HandleOnAdLoaded");
    }
}
