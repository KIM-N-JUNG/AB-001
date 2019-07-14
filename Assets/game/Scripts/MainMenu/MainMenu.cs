using System.Collections;
using System.Collections.Generic;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text mainMenuTitleUI;
    public GameObject mainMenuUI;
    public GameObject privacyBoardUI;
    public GameObject popupMenuUI;
    public Toggle toggle_login;
    public AndroidSet androidSet;
    public static UserInfo userInfo;
    private LoginManager loginManager = LoginManager.GetInstance;

    public void PlayGame()
    {
        SceneManager.LoadScene((int)Constant.SceneNumber.GAME);
    }

    public void Rankboard()
    {
        SceneManager.LoadScene((int)Constant.SceneNumber.RANK_BOARD);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void Awake()
    {
        // Font 설정
        mainMenuTitleUI.text = Properties.GetMainMenuTitle();
        mainMenuTitleUI.font = (Font)Resources.Load((string)Properties.GetMainMenuTitleFont());
        mainMenuTitleUI.fontSize = Properties.GetMainMenuTitleFontSize();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.MAIN_MENU)
            return;

        Debug.Log("MainMenu.Start()");

        toggle_login.isOn = SingletonClass.Instance.bLogin;
        loginManager.callback = new LoginManager.Callback();
        loginManager.callback.onUserNotExisted = OnUserNotExisted;
        loginManager.callback.onLogin = OnLogin;
        loginManager.callback.onLogout = OnLogout;
        loginManager.callback.onShowPrivacyBoard = OnShowPrivacyBoard;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.MAIN_MENU)
        {
            //if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    popupMenuUI.SetActive(true);
                    return;
                }
            }
        }
#endif
    }

    private void OnUserNotExisted()
    {
        Debug.Log("OnUserNotExisted() - Do register newbie");
        SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
    }

    private void OnLogin(User user)
    {
        R_UserGame userGame = null;
        try
        {
            Debug.Log("OnLogin() - MainMenu.userInfo check");
            Debug.Log(MainMenu.userInfo);
            Debug.Log(MainMenu.userInfo.user_id);
            userGame = R_UserGameService.Instance.GetUserGameByUserIdAndGameCode(user.user_id, Constant.GAME_CODE);
        }
        catch (DatabaseConnectionException e)
        {
            Debug.Log("###### Exception #########");
            Debug.Log(e.ToString());
            Debug.Log(Properties.GetDatabaseConnectionErrorMessage());
            Login(false);
            return;
        }

        userInfo.nick_name = userGame.nick_name;
        toggle_login.isOn = true;
        androidSet.ShowToast(Properties.GetLoginSucceedMessage() + " (" + userInfo.nick_name + ")", false);
    }

    private void OnLogout()
    {
        toggle_login.isOn = false;
    }

    private void OnShowPrivacyBoard(bool show)
    {
        Debug.Log("OnShowPrivacyBoard(" + !show + ")");
        mainMenuUI.SetActive(!show);
        privacyBoardUI.SetActive(show);
        loginManager.Login(!show);
    }

    public void Login(bool bLogin)
    {
        loginManager.Login(bLogin);
    }

    public void AgreePrivacy(bool agree)
    {
        loginManager.AgreePrivacy(agree);
    }

    public void AgreeService(bool agree)
    {
        loginManager.AgreeService(agree);
    }
}
