using System.Collections;
using Database.Dto;
using Database.Service;
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
        androidSet.ShowToast(Properties.GetLoginSucceedMessage() + " (" + user.nick_name + ")", false);
        Debug.Log("환영합니다 " + user.nick_name + "님. " + (user.visit_count + 1) + "번째 방문입니다.");
        int ret = UserService.Instance.UpdateUserByUserId(user.user_id, "visit_count", user.visit_count + 1);
        Debug.Log("ret is " + ret);

        loginFinish = true;
    }

    private void OnLogout()
    {
        // Unreachable
        androidSet.ShowToast(Properties.GetIndicateOfflineModeMessage(), false);
        loginFinish = true;
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
            yield return new WaitForSecondsRealtime(0.01f);
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
        yield return new WaitForSecondsRealtime(1.5f);
        oper.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = loadingValue + "%";
        sliderUI.value = (float)(loadingValue) / 100.0f;
    }
}
