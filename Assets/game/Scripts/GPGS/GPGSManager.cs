using UnityEngine;
using System.Collections;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;
using MySql.Data.MySqlClient;

public class UserInfo
{
    public string user_id { get; set; }
    public string auth { get; set; }
    public string user_name { get; set; }
    public string nick_name { get; set; }
    public string user_email { get; set; }
    public string user_image { get; set; }
    public int user_country { get; set; }

	public override string ToString()
	{
		return String.Format("GPGSManager.UserInfo [user_id: {0}, auth: {1}, user_name: {2}, nick_name: {3}, user_email: {4}, user_country: {5}"
			, user_id, auth, user_name, nick_name, user_email, user_country);
	}
}

public class GPGSManager : Singleton<GPGSManager>
{
    public delegate void OnAuthenticationCb(bool login, UserInfo userInfo);
    public delegate void OnSubmissionCb(bool success);
    public delegate void OnrevelationAchievementCb(bool success);
    public delegate void OnShowAchievement(bool success);

    public AndroidSet androidSet;

    public class Callback
    {
        public OnAuthenticationCb onAuthenticationCb { get; set; }
        public OnSubmissionCb onSubmissionCb { get; set; }
        public OnrevelationAchievementCb onRevelationAchievementCb { get; set; }
        public OnShowAchievement onShowAchievement { get; set; }
    }

    String[,] arrTimeArchivement;
    public Callback Cb = new Callback();
    int curIndexArchievement;

void Start()
{
    Debug.Log("### GPGSManager Start");

    #if UNITY_ANDROID
           PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                //.EnableSavedGames() // 저장된 게임
                .RequestEmail()
                // .RequestServerAuthCode(false)
                //.RequestIdToken()
                .Build();
           GooglePlayGames.PlayGamesPlatform.InitializeInstance(config);
        //    GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = true;
           GooglePlayGames.PlayGamesPlatform.Activate();
    #elif UNITY_IOS
                //    GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
    #endif

    // Select the Google Play Games platform as our social platform implementation
    // GooglePlayGames.PlayGamesPlatform.Activate();

    arrTimeArchivement
       = new String[,] {
                { "10", GPGSIds.achievement_10 },
                { "30", GPGSIds.achievement_30 },
                { "60", GPGSIds.achievement_1 },
                { "300", GPGSIds.achievement_5 },
                { "600", GPGSIds.achievement_10_2 } };

    curIndexArchievement = 0;
}

/// <summary>
/// GPGS를 초기화 합니다.
/// </summary>
public void InitializeGPGS()
{
    if (SingletonClass.Instance.bLogin)
    {
        Debug.Log("GPGSManager.InitializeGPGS() is logined (SingletonClass.Instance.bLogin : " + SingletonClass.Instance.bLogin + ")");
        LoginGPGS();
    }
    else
    {
        Debug.Log("GPGSManager.InitializeGPGS() - not logined");
        LogoutGPGS(true);
    }
}

/// <summary>
/// GPGS를 로그인 합니다.
/// </summary>
public void LoginGPGS()
{
    if (Application.internetReachability == NetworkReachability.NotReachable)
    {
        Debug.Log(Properties.GetIndicateOfflineModeMessage());
        LogoutGPGS(true);
        return;
    }

    // 로그인이 안되어 있으면
    if (!Social.localUser.authenticated)
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!! GPGSManager.LoginGPGS() : !Social.localUser.authenticated");
        Social.localUser.Authenticate((Action<bool>)((bool success) =>
        {
            Debug.Log("### Authentication successful");
            Debug.Log("Username: " + Social.localUser.userName);
            Debug.Log("ImageUrl: " + Social.localUser.image);
            Debug.Log("User ID: " + Social.localUser.id);
            string email = ((PlayGamesLocalUser)Social.localUser).Email;
            Debug.Log("Email: " + email);
            Debug.Log("IsUnderage: " + Social.localUser.underage);
            Debug.Log("Friends: " + Social.localUser.friends);

            UserInfo userInfo = new UserInfo();
            userInfo.user_id = Social.localUser.id;
            userInfo.user_email = email;
            userInfo.user_name = Social.localUser.userName;
            //userInfo.user_image = Social.localUser.image;
            userInfo.auth = "google";
            // TODO: needs to find country correctly
            userInfo.user_country = (int)Application.systemLanguage;
            this.Cb.onAuthenticationCb?.Invoke(success, userInfo);
        }));
    }
    else
    {
        //Debug.Log("### Social.localUser.authenticated");
        //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
        //Debug.Log("### _IDtoken : " + _IDtoken);

        //유저 토큰 받기 첫번째 방법
        //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
        //두번째 방법
        string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        //인증코드 받기
        string _authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
        //Debug.Log("### authcode : " + _authCode + " / " + "idtoken : " + _IDtoken);

        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!! GPGSManager.LoginGPGS() : !Social.localUser.authenticated else");
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
        //((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
        PlayGamesPlatform.Instance.SignOut();
        
        ((PlayGamesPlatform)Social.Active).SignOut();
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

public String getAchievementTime()
{
    if (Social.localUser.authenticated) {
        return arrTimeArchivement[curIndexArchievement, 0];
    } else {
        return "";
    }
}

// 업적, 리더보드
public void UnlockAchievement()
{
    if (!Social.localUser.authenticated) {
        return;
    }

    String name = arrTimeArchivement[curIndexArchievement, 1];
#if UNITY_ANDROID
    PlayGamesPlatform.Instance.ReportProgress(name, 100f, null);
    curIndexArchievement++;
#elif UNITY_IOS
    Social.ReportProgress("Score_100", 100f, null);
#endif
}

public void RevealAchievement(int time, Action<bool> callback)
{
    if (!Social.localUser.authenticated) {
        return;
    }

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
    if (!Social.localUser.authenticated)
    {
        // Social.localUser.Authenticate((bool success) =>
        // {
        //     if (success)
        //     {
        //             // Sign In 성공
        //             // 바로 업적 UI 표시 요청
        //             Social.ShowAchievementsUI();
        //         return;
        //     }
        //     else
        //     {
        //             // Sign In 실패 처리
        //             return;
        //     }
        // });
        return;
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
    Debug.Log("### ShowLeaderboardUI");

    // Sign In 이 되어있지 않은 상태라면
    // Sign In 후 리더보드 UI 표시 요청할 것
    if (!Social.localUser.authenticated)
    {
        Debug.Log("로그인 되어있지 않음");
        androidSet.ShowToast("로그인 후 확인할 수 있습니다.", false);
        return;
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
    Debug.Log("### PrintTokensPrintTokens");
    if (PlayGamesPlatform.Instance.localUser.authenticated)
    {
        //유저 토큰 받기 첫번째 방법
        //string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
        //두번째 방법
        string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();

        //인증코드 받기
        string _authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
    }
    else
    {
        Debug.Log("### 접속되어있지 않습니다. PlayGamesPlatform.Instance.localUser.authenticated :  fail");
    }
}

}
