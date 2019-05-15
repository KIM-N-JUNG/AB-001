using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject privacyBoardUI;
    public Toggle toggle_login;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
