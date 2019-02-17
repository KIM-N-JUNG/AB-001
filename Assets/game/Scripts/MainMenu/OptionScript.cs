using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    GameObject toggleBackgroundSoundToggle;
    public Toggle toggle_acc;
    public Toggle toggle_BGSound;
    public Toggle toggle_effectSound;
    public Slider slider_difficult;
    public GameObject OptionUI;

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
    }

    public void SetAcceleration(bool flag)
    {
        SingletonClass.Instance.acceleration = flag;
        Debug.Log("acceleration : " + flag);
    }

    public void SetBGSound(bool flag)
    {
        SingletonClass.Instance.bBGSound = flag;
        Debug.Log("bBGSound : " + flag);
    }

    public void SetEffectSound(bool flag)
    {
        SingletonClass.Instance.bEffectSound = flag;
        Debug.Log("bEffectSound : " + flag);
    }

    public void OnDifficultLevelChange()
    {
        SingletonClass.Instance.level = (int)slider_difficult.value;
        Debug.Log("Difficult Level : " + slider_difficult.value);
        Debug.Log("OptionUI : " + OptionUI.transform);
        GameObject obj = OptionUI.transform.Find("DifficultyName").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText(DifficultyName[SingletonClass.Instance.level]);
    }
}