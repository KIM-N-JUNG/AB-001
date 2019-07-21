using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using UnityEngine.UI;
using Ab001.Util;

public class ScoreUploader : MonoBehaviour
{
	public Timer timer;
	public Score _score;
	public GameObject inputPanelUI;
    public InputField messageTextUI;

    private bool checkStatus()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.GAME)
        {
            return false;
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log(Properties.GetIndicateOfflineModeMessage());
            return false;
        }
        // 로그인이 안되어있을 때 종료
        if (SingletonClass.Instance.bLogin == false)
        {
            return false;
        }
        return true;
    }

    public void ToggleInputUI(bool bShow)
    {
        if (!checkStatus())
            return;
        inputPanelUI.SetActive(bShow);
    }

    public void updateMyBestScore()
    {
        float time = timer.GetTime();
        int myScore = _score.GetScore();

        int ret = 0;
        if (MainMenu.myRankInfo.score == null)
        {
            // Insert a new score
            ret = ScoreService.Instance.InsertScore(new Ab001Score()
            {
                user_id = MainMenu.userInfo.user_id,
                score = myScore,
                message = messageTextUI.text,
                level = SingletonClass.Instance.level,
                time = time
            });
        } else
        {
            // Update a new score
            MainMenu.myRankInfo.score.score = myScore;
            MainMenu.myRankInfo.score.level = SingletonClass.Instance.level;
            MainMenu.myRankInfo.score.time = time;
            MainMenu.myRankInfo.score.message = messageTextUI.text;
            MainMenu.myRankInfo.score.score_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow();

            ret = ScoreService.Instance.UpdateScore(MainMenu.myRankInfo.score);
        }
        // 최고 점수이니 기록을 남기도록 한다

        Debug.Log("ret is " + ret);
        if (ret == 0)
        {
            Debug.Log("Insert score Error!");
        }
        ToggleInputUI(false);
    }

    public void RegisterScore()
    {
        // 로그인이 안되어있을 때 종료
        if (!checkStatus())
            return;

        float time = timer.GetTime();
        int myScore = _score.GetScore();

        if (MainMenu.myRankInfo.score != null)
        {
            // compare who are bigger score
            if (myScore <= MainMenu.myRankInfo.score.score) // 최고점수 아님. 무시한다.
            {
                Debug.Log("score is not higher");
                return;
            }
        }
        ToggleInputUI(true);
    }
}