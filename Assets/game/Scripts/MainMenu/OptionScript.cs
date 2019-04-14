using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    GameObject toggleBackgroundSoundToggle;
    public Toggle toggle_login;
    public Toggle toggle_acc;
    public Toggle toggle_BGSound;
    public Toggle toggle_effectSound;
    public Slider slider_difficult;
    public GameObject difficultyUI;
    public GameObject optionMenuUI;
    public GameObject privacyBoardUI;

    protected readonly string[] DifficultyName = {
        "EASY",
        "NORMAL",
        "HARD",
        "CRAZY"
    };

    void Start()
    {
        var ins = SingletonClass.Instance;

        toggle_login.isOn = ins.bLogin ? true : false;
        toggle_acc.isOn = ins.acceleration ? true : false;
        toggle_BGSound.isOn = ins.bBGSound ? true : false;
        toggle_effectSound.isOn = ins.bEffectSound ? true : false;
        slider_difficult.onValueChanged.AddListener(delegate { OnDifficultLevelChange(); });
        slider_difficult.value = ins.level;
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
        optionMenuUI.SetActive(!toogleShow);
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
        if (bLogin)
        {
            if (IsAgreePrivacy())
            {
                Debug.Log("LoginGPGS");
                GPGSManager.GetInstance.LoginGPGS();
            }
            else
            {
                ShowPrivacyBoard(true);
                return;
            }
        }
        else
        {
            Debug.Log("LogoutGPGS");
            AgreePrivacy(false);
            AgreeService(false);
            GPGSManager.GetInstance.LogoutGPGS(false);
        }

        Debug.Log("Login - " + bLogin);
        SingletonClass.Instance.bLogin = bLogin;
        PlayerPrefs.SetInt("login", bLogin ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetAcceleration(bool flag)
    {
        SingletonClass.Instance.acceleration = flag;
        Debug.Log("acceleration : " + flag);
        PlayerPrefs.SetInt("acceleration", flag ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetBGSound(bool flag)
    {
        SingletonClass.Instance.bBGSound = flag;
        Debug.Log("bBGSound : " + flag);
        PlayerPrefs.SetInt("bgSound", flag ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetEffectSound(bool flag)
    {
        SingletonClass.Instance.bEffectSound = flag;
        Debug.Log("bEffectSound : " + flag);
        PlayerPrefs.SetInt("effectSound", flag ? 1 : 0);
        PlayerPrefs.Save();

        Social.ShowLeaderboardUI();
    }

    public void OnDifficultLevelChange()
    {
        SingletonClass.Instance.level = (int)slider_difficult.value;
        Debug.Log("Difficult Level : " + slider_difficult.value);
        Debug.Log("OptionUI : " + difficultyUI.transform);
        GameObject obj = difficultyUI.transform.Find("DifficultyName").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText(DifficultyName[SingletonClass.Instance.level]);

        PlayerPrefs.SetInt("level", SingletonClass.Instance.level);
        PlayerPrefs.Save();
    }
}