using System.Collections;
using System.Collections.Generic;
using Database.Dto;
using Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("MainMenu.Start()");
        Debug.Log(SingletonClass.Instance.bLogin);
        toggle_login.isOn = SingletonClass.Instance.bLogin ? true : false;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Offline Mode");
            return;
        }

        // Do Login 
        GPGSManager.GetInstance.Cb.onAuthenticationCb = (bool success, UserInfo userInfo) =>
        {
            Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + userInfo.user_name);
            if (success)
            {
                androidSet.ShowToast("Login Success", false);
                // userId값으로 db에 query 
                //userInfo.user_id = "asdf";
                string query = "select * from user where user_id = " + "'" + userInfo.user_id + "'";
                User user = null;
                user = UserService.Instance.GetUserByUserId(userInfo.user_id);
                if (user == null)
                {
                    androidSet.ShowToast("맙소사.. 유저 정보가 없습니다! 버그 입니다!", false);
                    return;
                }

                androidSet.ShowToast("환영합니다 " + user.user_name + "님. " + (user.visit_count + 1) + "번째 방문입니다.", false);
                int ret = UserService.Instance.UpdateUserById(user.id, "visit_count", user.visit_count + 1);
                Debug.Log("ret is " + ret);
            }
            else
            {
                androidSet.ShowToast("Login Failed", false);
            }
        };

        GPGSManager.GetInstance.InitializeGPGS();
        Login(SingletonClass.Instance.bLogin);
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
        if (IsAgreePrivacy())
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
        if (IsAgreePrivacy())
        {
            ShowPrivacyBoard(false);
        }
    }

    public void Login(bool bLogin)
    {
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
                        androidSet.ShowToast("Login Success", false);
                        // userId값으로 db에 query 
                        User user = null;
                        user = UserService.Instance.GetUserByUserId(MainMenu.userInfo.user_id);
                        if (user == null)
                        {
                            SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
                            return;
                        }
                    }
                    else
                    {
                        androidSet.ShowToast("Login Failed", false);
                    }
                };

                GPGSManager.GetInstance.LoginGPGS();
                Debug.Log("LoginGPGS");
            }
            else
            {
                ShowPrivacyBoard(true);
                return;
            }
        }
        else // Logout
        {
            AgreePrivacy(false);
            AgreeService(false);
            GPGSManager.GetInstance.LogoutGPGS(false);
            Debug.Log("LogoutGPGS");
        }

        SingletonClass.Instance.bLogin = bLogin;
        PlayerPrefs.SetInt("login", bLogin ? 1 : 0);
        PlayerPrefs.Save();
    }
}
