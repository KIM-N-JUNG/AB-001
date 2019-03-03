using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;



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

    /// <summary>
    /// GPGS를 초기화 합니다.
    /// </summary>
    public void InitializeGPGS()
    {
        bLogin = false;

        PlayGamesPlatform.Activate();
    }

    /// <summary>
    /// GPGS를 로그인 합니다.
    /// </summary>
    public void LoginGPGS()
    {
        // 로그인이 안되어 있으면
        if (!Social.localUser.authenticated)
        {
            Debug.Log("!Social.localUser.authenticated");
            Social.localUser.Authenticate(LoginCallBackGPGS);
        } else
        {
            Debug.Log("Social.localUser.authenticated");
        }
    }

    /// <summary>
    /// GPGS Login Callback
    /// </summary>
    /// <param name="result"> 결과 </param>
    public void LoginCallBackGPGS(bool result)
    {
        bLogin = result;
    }

    /// <summary>
    /// GPGS를 로그아웃 합니다.
    /// </summary>
    public void LogoutGPGS()
    {
        // 로그인이 되어 있으면
        if (Social.localUser.authenticated)
        {
            ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            bLogin = false;
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
}
