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

    private const string BANNER_ID = "ca-app-pub-1339724987571025/6657855346";
    private const string BANNER_ID_TEST = "ca-app-pub-3940256099942544/6300978111";

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        PauseUI.SetActive(false);

        RequestBanner();
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
            bannerView.Show();
            bAdsShow = true;
        }
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = BANNER_ID;
        //string adUnitId = BANNER_ID_TEST;
#elif UNITY_IPHONE
            //string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            //string adUnitId = "unexpected_platform";
#endif

        // #type1
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.Bottom);

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
        paused = false;
        timer.Resume();
        score.Resume();
    }

    public void Restart()
    {
        Debug.Log("Restart");
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

    private void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
        Debug.Log("HandleOnAdLoaded");
        bAdsLoaded = true;
        bAdsShow = false;
        bannerView.Hide();
    }

}
