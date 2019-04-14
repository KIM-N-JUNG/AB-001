using UnityEngine;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;


public class GPGSManager : Singleton<GPGSManager>
{
    /// <summary>
    /// 현재 로그인 중인지 체크
    /// </summary>
    public bool bLogin
    {
        get;
        set;
    }

    int[,] arrArchivement;

    void Start()
    {
        Debug.Log("### GPGSManager Start");
        bLogin = false;

        //#if UNITY_ANDROID
        //        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //             //.EnableSavedGames() // 저장된 게임
        //             .RequestEmail()
        //             .RequestServerAuthCode(false)
        //             //.RequestIdToken()
        //             .Build();
        //        PlayGamesPlatform.InitializeInstance(config);
        //        PlayGamesPlatform.DebugLogEnabled = true;
        //        PlayGamesPlatform.Activate();
        //#elif UNITY_IOS
        //                GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
        //#endif

        // Select the Google Play Games platform as our social platform implementation
        GooglePlayGames.PlayGamesPlatform.Activate();

        arrArchivement = new int[,] { {10, 0}, { 30, 0}, { 60, 0}, { 300, 0}, { 600, 0} };
    }

    /// <summary>
    /// GPGS를 초기화 합니다.
    /// </summary>
    public void InitializeGPGS()
    {
        bLogin = false;

    }

    /// <summary>
    /// GPGS를 로그인 합니다.
    /// </summary>
    public void LoginGPGS()
    {
        // 로그인이 안되어 있으면
        if (!Social.localUser.authenticated)
        {
            //Debug.Log("### !Social.localUser.authenticated");
            //Social.localUser.Authenticate(LoginCallBackGPGS);

            Social.localUser.Authenticate((bool success) =>
            {
                Debug.Log("### Authentication successful");
                Debug.Log("Username: " + Social.localUser.userName);
                //Debug.Log("Username: ");
                Debug.Log("ImageUrl: " + Social.localUser.image);
                Debug.Log("User ID: " + Social.localUser.id);
                string email = ((PlayGamesLocalUser)Social.localUser).Email;
                Debug.Log("Email: " + email);
                Debug.Log("IsUnderage: " + Social.localUser.underage);
                Debug.Log("Friends: " + Social.localUser.friends);
            });
        }
        else
        {
            Debug.Log("### Social.localUser.authenticated");

            //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            //Debug.Log("### _IDtoken : " + _IDtoken);

            //유저 토큰 받기 첫번째 방법
            //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            //두번째 방법
            string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

            //인증코드 받기
            string _authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            Debug.Log("### authcode : " + _authCode + " / " + "idtoken : " + _IDtoken);
        }
    }

    /// <summary>
    /// GPGS Login Callback
    /// </summary>
    /// <param name="result"> 결과 </param>
    public void LoginCallBackGPGS(bool result)
    {
        bLogin = result;

        if (result)
        {
            Debug.Log("### Authentication successful");
            //Debug.Log("Username: " + Social.localUser.userName);
            Debug.Log("Username: ");
            Debug.Log("ImageUrl: " + Social.localUser.image);
            Debug.Log("User ID: " + Social.localUser.id);
            string email = ((PlayGamesLocalUser)Social.localUser).Email;
            Debug.Log("Email: " + email);
            Debug.Log("IsUnderage: " + Social.localUser.underage);
            Debug.Log("Friends: " + Social.localUser.friends);
        }
        else
        {
            Debug.Log("### Authentication failed");
        }
    }

    /// <summary>
    /// GPGS를 로그아웃 합니다.
    /// </summary>
    public void LogoutGPGS(bool bForced)
    {
        Debug.Log("### call LogoutGPGS");

        // 로그인이 되어 있으면
        if (Social.localUser.authenticated || bForced)
        {
            bLogin = false;
            //((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            PlayGamesPlatform.Instance.SignOut();

            Debug.Log("### call SignOut 1");

            ((PlayGamesPlatform)Social.Active).SignOut();

            Debug.Log("### call SignOut 2");
        }
    }

    /// <summary>
    /// GPGS에서 자신의 프로필 이미지를 가져옵니다.
    /// </summary>
    /// <returns> Texture 2D 이미지 </returns>
    public Texture2D GetImageGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.image;
        else
            return null;
    }

    /// <summary>
    /// GPGS 에서 사용자 이름을 가져옵니다.
    /// </summary>
    /// <returns> 이름 </returns>
    public string GetNameGPGS()
    {
        if (Social.localUser.authenticated)
            return Social.localUser.userName;
        else
            return null;
    }

    public void SignoutGPGS()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    // 업적, 리더보드
    public void UnlockAchievement(int time)
    {
        String name = "";
        switch (time)
        {
            case 10:
                name = GPGSIds.achievement_10;
                break;
            case 30:
                name = GPGSIds.achievement_30;
                break;
            case 60:
                name = GPGSIds.achievement_1;
                break;
            case 300:
                name = GPGSIds.achievement_5;
                break;
            case 600:
                name = GPGSIds.achievement_10_2;
                break;
        }
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ReportProgress(name, 100f, null);
#elif UNITY_IOS
            Social.ReportProgress("Score_100", 100f, null);
#endif
    }

    public void RevealAchievement(int time, Action<bool> callback) {
        switch (time)
        {
            case 10:
                name = GPGSIds.achievement_10;
                break;
            case 30:
                name = GPGSIds.achievement_30;
                break;
            case 60:
                name = GPGSIds.achievement_1;
                break;
            case 300:
                name = GPGSIds.achievement_5;
                break;
            case 600:
                name = GPGSIds.achievement_10_2;
                break;
        }
#if UNITY_ANDROID
        //PlayGamesPlatform.Instance.RevealAchievement(name, 
        //(bool success) => {
        //    if (success)
        //    {

        //        return;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //});
        PlayGamesPlatform.Instance.RevealAchievement(name, callback);
#elif UNITY_IOS
            Social.ReportProgress("Score_100", 100f, null);
#endif
    }

    public void ShowAchievementUI()
    {
        // Sign In 이 되어있지 않은 상태라면
        // Sign In 후 업적 UI 표시 요청할 것
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                    // 바로 업적 UI 표시 요청
                    Social.ShowAchievementsUI();
                    return;
                }
                else
                {
                    // Sign In 실패 처리
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }


    /// <summary>
    /// 리더보드에 점수를 등록한다.
    /// </summary>
    public void SubmitToLeaderBorad(int score)
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("### Update Score Success");
                }
                else
                {
                    Debug.Log("### Update Score Fail");
                }
            });
        }
        else
        {
            Debug.Log("### Need Log in");
        }
#elif UNITY_IOS
        Social.ReportScore(score, "Leaderboard_ID", (bool success) =>
            {
                if (success)
                {
                    // Report 성공
                    // 그에 따른 처리
                }
                else
                {
                    // Report 실패
                    // 그에 따른 처리
                }
            });
#endif
    }

    public void ShowLeaderboardUI()
    {
        // Sign In 이 되어있지 않은 상태라면
        // Sign In 후 리더보드 UI 표시 요청할 것
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                    // 바로 리더보드 UI 표시 요청
                    Social.ShowLeaderboardUI();
                    return;
                }
                else
                {
                    // Sign In 실패 
                    // 그에 따른 처리
                    return;
                }
            });
        }

#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
#elif UNITY_IOS
        GameCenterPlatform.ShowLeaderboardUI("Leaderboard_ID", UnityEngine.SocialPlatforms.TimeScope.AllTime);
#endif
    }

    /// <summary>
    /// 유저의 토큰을 출력한다.
    /// </summary>
    /// <returns></returns>
    public void PrintTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            //유저 토큰 받기 첫번째 방법
            //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            //두번째 방법
            string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

            //인증코드 받기
            string _authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
            Debug.Log("### authcode : " + _authCode + " / " + "idtoken : " + _IDtoken);
        }
        else
        {
            Debug.Log("### 접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
        }
    }

}
