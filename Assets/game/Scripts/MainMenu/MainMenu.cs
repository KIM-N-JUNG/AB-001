using System.Collections;
using System.Collections.Generic;
using Database.Dto;
using Database.Service;
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

    private void OnLogin(User user)
    {
        toggle_login.isOn = true;
        androidSet.ShowToast(Properties.GetLoginSucceedMessage() + " (" + user.nick_name + ")", false);
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
