using Database.Dto;
using Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager
{
    public delegate void OnLogin(User user);
    public delegate void OnLogout();
    public delegate void OnShowPrivacyBoard(bool show);

    public class Callback
    {
        public object data;
        public OnLogin onLogin = null;
        public OnLogout onLogout = null;
        public OnShowPrivacyBoard onShowPrivacyBoard = null;
    }
    public Callback callback { get; set; }
    private static LoginManager instance = null;
    public static LoginManager GetInstance
    {
        get
        {
            if (instance == null)
            {
                lock (typeof(MySqlConnector))
                {
                    if (instance == null)
                    {
                        instance = new LoginManager();
                    }
                }
            }
            return instance;
        }
    }
    private static UserInfo userInfo { get; set; }

    private LoginManager()
    {
    }

    public void Login(bool bLogin)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            AgreePrivacy(false);
            AgreeService(false);
            GPGSManager.GetInstance.LogoutGPGS(false);
            SingletonClass.Instance.bLogin = false;
            PlayerPrefs.SetInt("bLogin", 0);
            PlayerPrefs.Save();

            callback.onLogout?.Invoke();
            return;
        }

        Debug.Log("Login() bLogin - " + bLogin);

        if (bLogin)
        {
            if (IsAgreePrivacy() == false)
            {
                callback.onShowPrivacyBoard?.Invoke(true);
                return;
            }

            GPGSManager.GetInstance.Cb.onAuthenticationCb = (bool success, UserInfo _userInfo) =>
            {
                Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + _userInfo.user_name);
                // Test용
                //_userInfo.user_id = "1234";
                //_userInfo.user_name = "asdfasdf";
                //_userInfo.user_email = "jsy7787@gmail.com";
                //_userInfo.user_image = "";

                MainMenu.userInfo = _userInfo;
                if (success)
                //if (true)
                {
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
                        Debug.Log(Properties.GetDatabaseConnectionErrorMessage());
                        Login(false);
                        return;
                    }

                    // 최초 로그인
                    if (user == null)
                    {
                        MainMenu.userInfo.is_legacy_user = false;
                        SceneManager.LoadScene((int)Constant.SceneNumber.PROLOGUE);
                    }

                    Debug.Log("User nickName : " + user.nick_name);
                    SingletonClass.Instance.bLogin = true;
                    PlayerPrefs.SetInt("bLogin", SingletonClass.Instance.bLogin ? 1 : 0);
                    PlayerPrefs.Save();
                    Debug.Log("SingletonClass.Instance.bLogin is " + SingletonClass.Instance.bLogin);

                    callback.onLogin?.Invoke(user);
                }
                else
                {
                    Login(false);
                }
            };
            // DO LOGIN!!!
            GPGSManager.GetInstance.LoginGPGS();
        }
        else // Logout
        {
            AgreePrivacy(false);
            AgreeService(false);
            GPGSManager.GetInstance.LogoutGPGS(false);
            Debug.Log("LogoutGPGS");
            SingletonClass.Instance.bLogin = false;
            Debug.Log("SingletonClass.Instance.bLogin is " + SingletonClass.Instance.bLogin);
            PlayerPrefs.SetInt("bLogin", SingletonClass.Instance.bLogin ? 1 : 0);
            PlayerPrefs.Save();
            PlayerPrefs.DeleteKey("bLogin");
            callback.onLogout?.Invoke();
        }
    }

    private bool IsAgreePrivacy()
    {
        bool isAgree = SingletonClass.Instance.bPrivacyAgreement && SingletonClass.Instance.bServiceAgreement;
        Debug.Log("IsAgreePrivacy & Service? : " + isAgree);
        return isAgree;
    }

    public void AgreePrivacy(bool agree)
    {
        Debug.Log("AgreePrivacy(" + agree + ")");
        SingletonClass.Instance.bPrivacyAgreement = agree;
        PlayerPrefs.SetInt("privacy", agree ? 1 : 0);
        PlayerPrefs.Save();
        if (agree == true && IsAgreePrivacy())
        {
            callback.onShowPrivacyBoard?.Invoke(false);
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
            callback.onShowPrivacyBoard?.Invoke(false);
        }
    }
}
