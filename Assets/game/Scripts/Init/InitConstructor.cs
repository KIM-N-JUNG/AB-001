using System;
using System.Collections;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using Ab001.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitConstructor : MonoBehaviour
{
    public Text titleUI;
    public Text textUI;
    public Slider sliderUI;
    public bool enableDebug;
    public Text debugTextUI;
    public AndroidSet androidSet;

    private int loadingValue = 1;
    private bool loginFinish = false;

    private LoginManager loginManager = LoginManager.GetInstance;

    private void OnLogin(User user)
    {
        R_UserGame userGame = null;
        try
        {
            Debug.Log("OnLogin() - MainMenu.userInfo check");
            userGame = R_UserGameService.Instance.GetUserGameByUserIdAndGameCode(user.user_id, Constant.GAME_CODE);
        }
        catch (DatabaseConnectionException e)
        {
            Debug.Log("###### Exception #########");
            Debug.Log(e.ToString());
            Debug.Log(Properties.GetDatabaseConnectionErrorMessage());
            return;
        }

        MainMenu.userInfo.nick_name = userGame.nick_name;
        androidSet.ShowToast(Properties.GetLoginSucceedMessage() + " (" + MainMenu.userInfo.nick_name + ")", false);
        Debug.Log("환영합니다 " + MainMenu.userInfo.nick_name + "님. " + (user.visit_count + 1) + "번째 방문입니다.");
        int ret = UserService.Instance.UpdateUserByUserId(user.user_id, "visit_count", user.visit_count + 1);
        Debug.Log("ret is " + ret);

        loginFinish = true;
    }

    private void OnLogout()
    {
        // Unreachable
        androidSet.ShowToast("offline mode", false);
        loginFinish = true;
    }

    private void OnUserNotExisted()
    {
        // Logined but user is null
        // 로그인 되어있지만 유저정보가 사라진 상황
        Debug.Log("There is no user but logined.. go to logout");
        loginManager.Login(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("InitConstructor Start");
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.INIT)
            return;
        
        Debug.Log("Init - Start() DoLogin");

        // Do Login
        loginManager.callback = new LoginManager.Callback();
        loginManager.callback.onLogin = OnLogin;
        loginManager.callback.onLogout = OnLogout;
        loginManager.callback.onUserNotExisted = OnUserNotExisted;
        StartCoroutine(DoLogin());
    }

    private IEnumerator DoLogin()
    {
        Debug.Log("DoLogin() - " + SingletonClass.Instance.bLogin);
        yield return new WaitForSecondsRealtime(0.5f);

        if (SingletonClass.Instance.bLogin)
        {
            loginManager.Login(SingletonClass.Instance.bLogin);
        }
        else
        {
            loginFinish = true;
        }

        while (loadingValue <= 70)
        {
            yield return null;
            loadingValue++;
        }
        yield return new WaitUntil(() => loginFinish == true);
        yield return StartCoroutine(DoLoadScene());
    }

    private IEnumerator DoLoadScene()
    {
        Debug.Log("DoLoadScene");
        AsyncOperation oper = SceneManager.LoadSceneAsync((int)Constant.SceneNumber.MAIN_MENU);
        oper.allowSceneActivation = false;

        while (oper.isDone == false)
        {
            if (loadingValue < 99)
                loadingValue++;

            yield return null;

            if (oper.progress >= 0.9f)
            {
                loadingValue = 100;
                break;
            }
        }
        titleUI.text = "COMPLETE!";
        yield return new WaitForSecondsRealtime(1.0f);
        oper.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = loadingValue + "%";
        sliderUI.value = (float)(loadingValue) / 100.0f;
    }
}
