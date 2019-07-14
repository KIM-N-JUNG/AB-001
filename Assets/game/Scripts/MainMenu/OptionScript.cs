using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using Ab001.Database.Service;

public class OptionScript : MonoBehaviour
{

    public Toggle toggle_acc;
    public Toggle toggle_BGSound;
    public Toggle toggle_effectSound;
    public Slider slider_difficult;
    public GameObject difficultyUI;
    public AndroidSet androidSet;

    // for AdMob
    private BannerView bannerView;
    bool bAdsLoaded = false;

    protected readonly string[] DifficultyName = {
        "EASY",
        "NORMAL",
        "HARD",
        "CRAZY"
    };
    private const string APP_ID = "ca-app-pub-1339724987571025~1648266314";

    private const string BANNER_ID = "ca-app-pub-1339724987571025/3803064438";

    void Start()
    {
        var ins = SingletonClass.Instance;

        toggle_acc.isOn = ins.acceleration ? true : false;
        toggle_BGSound.isOn = ins.bBGSound ? true : false;
        toggle_effectSound.isOn = ins.bEffectSound ? true : false;
        slider_difficult.onValueChanged.AddListener(delegate { OnDifficultLevelChange(); });
        slider_difficult.value = ins.level;

        initBanner();
    }

    public void SetAcceleration(bool flag)
    {
        SingletonClass.Instance.acceleration = flag;
        Debug.Log("acceleration : " + flag);
        PlayerPrefs.SetInt("acceleration", flag ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetBGSound(bool flag)
    {
        SingletonClass.Instance.bBGSound = flag;
        Debug.Log("bBGSound : " + flag);
        PlayerPrefs.SetInt("bgSound", flag ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetEffectSound(bool flag)
    {
        SingletonClass.Instance.bEffectSound = flag;
        Debug.Log("bEffectSound : " + flag);
        PlayerPrefs.SetInt("effectSound", flag ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnDifficultLevelChange()
    {
        SingletonClass.Instance.level = (int)slider_difficult.value;
        Debug.Log("Difficult Level : " + slider_difficult.value);
        Debug.Log("OptionUI : " + difficultyUI.transform);
        GameObject obj = difficultyUI.transform.Find("DifficultyName").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText(DifficultyName[SingletonClass.Instance.level]);

        PlayerPrefs.SetInt("level", SingletonClass.Instance.level);
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        Debug.Log("Quit");

        if (bAdsLoaded)
        {
            bannerView.Hide();
        }
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
        MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("HandleOnAdLoaded");
        bAdsLoaded = true;
        bannerView.Show();
    }
}