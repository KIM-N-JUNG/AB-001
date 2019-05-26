using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    public Timer timer;
    public Score score;
    public Text PanelText;
    public GameObject starField;
    public GameObject plane;

    private bool paused = false;
    
    // for AdMob
    private BannerView bannerView;
    bool bAdsLoaded = false;
    bool bAdsShow = false;

    private const string APP_ID = "ca-app-pub-1339724987571025~1648266314";

    private const string BANNER_ID = "ca-app-pub-1339724987571025/9830363880";
    private const string BANNER_ID_TEST = "ca-app-pub-3940256099942544/6300978111";

    // Use this for initialization
    void Start () {
        Debug.Log("PauseMenu Start");

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        PauseUI.SetActive(false);
    
        // initAds();
        initBanner();
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        } else 
        {
            PauseUI.SetActive(false);
            //Time.timeScale = 1f;

            int onControl = PlayerPrefs.GetInt("onControl");
            if (onControl == 1)
            {
                Time.timeScale = 1f;
            } else
            {
                Time.timeScale = 0.3f;
            }

            if (bAdsShow)
            {
                bannerView.Hide();
                bAdsShow = false;
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");

        timer.Pause();
        score.Pause();

        float time = timer.GetTime();
        int s = score.GetScore();

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, s);

        paused = true;

        if (bAdsLoaded)
        {
            Debug.Log("bAdsLoaded : " + bAdsLoaded + ", call bannerView.Show()");
            bannerView.Show();
            bAdsShow = true;
        } else {
            bannerView.Show();
            Debug.Log("bAdsLoaded : " + bAdsLoaded);
        }
    }

 public void initAds()
    {
        Debug.Log("PauseMenu initAds");
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
    
    private void initBanner() {
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

    private void RequestBanner()
    {
        Debug.Log("RequestBanner");
#if UNITY_ANDROID
        string adUnitId = BANNER_ID;
        //string adUnitId = BANNER_ID_TEST;
#elif UNITY_IPHONE
            //string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            //string adUnitId = "unexpected_platform";
#endif
        Debug.Log("RequestBanner adUnitId : " + adUnitId);

        // #type1
        // Create a 320x50 banner at the top of the screen.
        // bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom);
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // #type2, 맞춤광고
        //AdSize adSize = new AdSize(250, 250);
        //BannerView bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;

        // 핸들러 지정
        if (false)
        {
            //// Called when an ad request failed to load.
            //bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            //// Called when an ad is clicked.
            //bannerView.OnAdOpening += HandleOnAdOpened;
            //// Called when the user returned from the app after an ad click.
            //bannerView.OnAdClosed += HandleOnAdClosed;
            //// Called when the ad click caused the user to leave the application.
            //bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

            // 실제 핸들러
            //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
            //{
            //    MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
            //                        + args.Message);
            //}

            //public void HandleOnAdOpened(object sender, EventArgs args)
            //{
            //    MonoBehaviour.print("HandleAdOpened event received");
            //}

            //public void HandleOnAdClosed(object sender, EventArgs args)
            //{
            //    MonoBehaviour.print("HandleAdClosed event received");
            //}

            //public void HandleOnAdLeavingApplication(object sender, EventArgs args)
            //{
            //    MonoBehaviour.print("HandleAdLeavingApplication event received");
            //}
        }

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void Resume()
    {
        Debug.Log("Resume");
        bannerView.Hide();
        bAdsShow = false;
        paused = false;
        timer.Resume();
        score.Resume();
    }

    public void Restart()
    {
        Debug.Log("Restart");
        bannerView.Hide();
        bAdsShow = false;
        SceneManager.LoadScene("AvoidBullets");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene(0);
    }

    public void End()
    {
        float time = timer.GetTime();
        int s = score.GetScore();

        GameObject obj = PauseUI.transform.Find("PauseText").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText("Game Over");

        GameObject btnResume = PauseUI.transform.Find("ResumeButton").gameObject;
        btnResume.SetActive(false);

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, s);

        paused = true;

        GPGSManager.GetInstance.SubmitToLeaderBorad(s);

        if (bAdsLoaded)
        {
            bannerView.Show();
            bAdsShow = true;
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");
        paused = false;
        starField.SetActive(false);
        plane.SetActive(false);
        MainMenu();

        if (bAdsLoaded)
        {
            bannerView.Hide();
            bAdsShow = false;
        }
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        Debug.Log("HandleOnAdLoaded");
        bAdsLoaded = true;
        bAdsShow = false;

        if (paused) {
            bannerView.Show();
            bAdsShow = true;
        } else {
            bannerView.Hide();
            bAdsShow = false;
        }
    }
}
