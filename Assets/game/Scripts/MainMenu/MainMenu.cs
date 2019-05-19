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
        toggle_login.isOn = SingletonClass.Instance.bLogin ? true : false;
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
        //toggle_login.isOn = false;
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
        if (SingletonClass.Instance.bLogin == bLogin)
        {
            return;
        }

        Debug.Log("bLogin - " + bLogin);

        if (bLogin)
        {
            if (IsAgreePrivacy())
            {
                GPGSManager.GetInstance.Cb.onAuthenticationCb = (bool success, UserInfo userInfo) =>
                {
                    Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + userInfo.user_name);
                    //userInfo.user_id = "1234";
                    //userInfo.user_name = "지운파파";
                    //userInfo.user_email = "jsy7787@gmail.com";
                    //userInfo.user_image = "";
                    if (success)
                    //if (true)
                    {
                        androidSet.ShowToast("Login Success", false);
                        // userId값으로 db에 query 
                        string query = "select * from user where user_id = " + "'" + userInfo.user_id + "'";
                        User user = null;
                        user = UserService.Instance.GetUserByUserId(userInfo.user_id);
                        if (user == null)
                        {
                            // user 등록
                            //user = new User();
                            user = new User
                            {
                                id = 0,
                                country = "Korea",
                                visit_count = 1,
                                user_id = userInfo.user_id,
                                email = userInfo.user_email,
                                user_image = userInfo.user_image,
                                user_name = userInfo.user_name
                            };

                            Debug.Log("insert User");
                            int r = UserService.Instance.InsertUser(user);
                            Debug.Log("ret is " + r);
                            // 프로필 입력 scene으로 이동
                            Debug.Log("There is no user -> newbie");
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
        else
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
