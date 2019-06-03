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

    public void PlayGame()
    {
        SceneManager.LoadScene((int)Constant.SceneNumber.GAME);
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
        Debug.Log("MainMenu.Start()");
        Debug.Log(SingletonClass.Instance.bLogin);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            androidSet.ShowToast(Properties.GetIndicateOfflineModeMessage(), false);
            Login(false);
            return;
        }

        // Do Login 
        GPGSManager.GetInstance.Cb.onAuthenticationCb = (bool success, UserInfo _userInfo) =>
        {
            Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + _userInfo.user_name);
            // For DEBUGGING
            //_userInfo.user_id = "1234";
            MainMenu.userInfo = _userInfo;
            if (success)
            //if (true)
            {
                androidSet.ShowToast(Properties.GetLoginSucceedMessage(), false);

                User user = null;
                try
                {
                    user = UserService.Instance.GetUserByUserId(MainMenu.userInfo.user_id);
                }
                catch (DatabaseConnectionException e)
                {
                    Debug.Log(e.ToString());
                    Login(false);
                    androidSet.ShowToast(Properties.GetDatabaseConnectionErrorMessage(), false);
                    return;
                }

                if (user == null)
                {
                    Debug.Log("유저 정보가 초기화 되었습니다. 다시 로그인 해 주세요.");
                    Login(false);
                    return;
                }

                if (user.nick_name != null && user.nick_name.Length <= 0)
                {
                    MainMenu.userInfo.is_legacy_user = true;
                    // 이전 등록 유저 (닉네임을 등록)
                    androidSet.ShowToast(Properties.GetProceedToPrologueMessage(), false);
                    SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
                    return;
                }
                Debug.Log("환영합니다 " + user.user_name + "님. " + (user.visit_count + 1) + "번째 방문입니다.");
                int ret = UserService.Instance.UpdateUserByUserId(user.user_id, "visit_count", user.visit_count + 1);
                Debug.Log("ret is " + ret);
            }
            else
            {
                androidSet.ShowToast(Properties.GetLoginFailedMessage(), false);
                Login(false);
            }
        };

        GPGSManager.GetInstance.InitializeGPGS();
        toggle_login.isOn = SingletonClass.Instance.bLogin;
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
                    mainMenuUI.SetActive(false);
                    popupMenuUI.SetActive(true);
                    return;
                }
            }
        }
#endif
    }

    private bool IsAgreePrivacy()
    {
        bool isAgree = SingletonClass.Instance.bPrivacyAgreement && SingletonClass.Instance.bServiceAgreement;
        Debug.Log("IsAgreePrivacy? : " + isAgree);
        return isAgree;
    }

    private void ShowPrivacyBoard(bool toogleShow)
    {
        Debug.Log("ShowPrivacyBoard()");
        mainMenuUI.SetActive(!toogleShow);
        privacyBoardUI.SetActive(toogleShow);
        Login(!toogleShow);
    }

    public void AgreePrivacy(bool agree)
    {
        Debug.Log("AgreePrivacy(" + agree + ")");
        SingletonClass.Instance.bPrivacyAgreement = agree;
        PlayerPrefs.SetInt("privacy", agree ? 1 : 0);
        PlayerPrefs.Save();
        if (agree == true && IsAgreePrivacy())
        {
            ShowPrivacyBoard(false);
        }
    }

    public void AgreeService(bool agree)
    {
        Debug.Log("AgreeService(" + agree + ")");
        SingletonClass.Instance.bServiceAgreement = agree;
        PlayerPrefs.SetInt("service", agree ? 1 : 0);
        PlayerPrefs.Save();
        if (agree == true && IsAgreePrivacy())
        {
            ShowPrivacyBoard(false);
        }
    }

    public void Login(bool bLogin)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            androidSet.ShowToast(Properties.GetIndicateOfflineModeMessage(), false);
            AgreePrivacy(false);
            AgreeService(false);
            GPGSManager.GetInstance.LogoutGPGS(false);
            SingletonClass.Instance.bLogin = false;
            PlayerPrefs.SetInt("bLogin", 0);
            PlayerPrefs.Save();
            return;
        }

        Debug.Log("bLogin - " + bLogin);
        if (bLogin == true && bLogin == SingletonClass.Instance.bLogin)
        {
            return;
        }

        if (bLogin)
        {
            if (IsAgreePrivacy())
            {
                GPGSManager.GetInstance.Cb.onAuthenticationCb = (bool success, UserInfo _userInfo) =>
                {
                    Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + _userInfo.user_name);
                    // Test용
                    //_userInfo.user_id = "1234";
                    //_userInfo.user_name = "지운파파";
                    //_userInfo.user_email = "jsy7787@gmail.com";
                    //_userInfo.user_image = "";

                    MainMenu.userInfo = _userInfo;
                    if (success)
                    //if (true)
                    {
                        androidSet.ShowToast(Properties.GetLoginSucceedMessage(), false);
                        // userId값으로 db에 query 
                        User user = null;
                        try
                        {
                            user = UserService.Instance.GetUserByUserId(MainMenu.userInfo.user_id);
                        }
                        catch (DatabaseConnectionException e)
                        {
                            Debug.Log("###### Exception #########");
                            Debug.Log(e.ToString());
                            androidSet.ShowToast(Properties.GetDatabaseConnectionErrorMessage(), false);
                            Login(false);
                            return;
                        }

                        // 최초 로그인
                        if (user == null)
                        {
                            MainMenu.userInfo.is_legacy_user = false;
                            SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
                            return;
                        }
                        // 기존 유저, 닉네임이 없음
                        else if (user.nick_name != null && user.nick_name.Length <= 0)
                        {
                            MainMenu.userInfo.is_legacy_user = true;
                            Debug.Log("기존 유저 입니다. 프롤로그로 이동 합니다");
                            SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
                            return;
                        }
                        // 이미 등록된 유저
                        else
                        {
                        }
                    }
                    else
                    {
                        Login(false);
                        return;
                    }
                };

                // DO LOGIN!!!
                GPGSManager.GetInstance.LoginGPGS();
                Debug.Log("LoginGPGS");

                SingletonClass.Instance.bLogin = bLogin;
                toggle_login.isOn = SingletonClass.Instance.bLogin;
                PlayerPrefs.SetInt("bLogin", bLogin ? 1 : 0);
                PlayerPrefs.Save();
                return;
            }
            else
            {
                ShowPrivacyBoard(true);
                return;
            }
        }

        // Logout
        AgreePrivacy(false);
        AgreeService(false);
        GPGSManager.GetInstance.LogoutGPGS(false);
        Debug.Log("LogoutGPGS");
        SingletonClass.Instance.bLogin = bLogin;
        Debug.Log("SingletonClass.Instance.bLogin is " + SingletonClass.Instance.bLogin);
        PlayerPrefs.SetInt("bLogin", bLogin ? 1 : 0);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("bLogin");
        toggle_login.isOn = SingletonClass.Instance.bLogin;
    }
}
