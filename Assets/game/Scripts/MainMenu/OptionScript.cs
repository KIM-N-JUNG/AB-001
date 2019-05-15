using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{

    public Toggle toggle_acc;
    public Toggle toggle_BGSound;
    public Toggle toggle_effectSound;
    public Slider slider_difficult;
    public GameObject difficultyUI;
    public AndroidSet androidSet;

    protected readonly string[] DifficultyName = {
        "EASY",
        "NORMAL",
        "HARD",
        "CRAZY"
    };

    void Start()
    {
        var ins = SingletonClass.Instance;

        toggle_acc.isOn = ins.acceleration ? true : false;
        toggle_BGSound.isOn = ins.bBGSound ? true : false;
        toggle_effectSound.isOn = ins.bEffectSound ? true : false;
        slider_difficult.onValueChanged.AddListener(delegate { OnDifficultLevelChange(); });
        slider_difficult.value = ins.level;
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