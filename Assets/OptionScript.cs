using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    GameObject toggleBackgroundSoundToggle;
    public Toggle toggle_acc;
    public Toggle toggle_BGSound;
    public Toggle toggle_effectSound;

    void Start()
    {
        var ins = SingletonClass.Instance;

        toggle_acc.isOn = ins.acceleration ? true : false;
        toggle_BGSound.isOn = ins.bBGSound ? true : false;
        toggle_effectSound.isOn = ins.bEffectSound ? true : false;
    }

    public void SetAcceleration(bool flag)
    {
        SingletonClass.Instance.acceleration = flag;
        Debug.Log("acceleration : " + flag);
    }

    public void setBGSound(bool flag)
    {
        SingletonClass.Instance.bBGSound = flag;
        Debug.Log("bBGSound : " + flag);
    }

    public void setEffectSound(bool flag)
    {
        SingletonClass.Instance.bEffectSound = flag;
        Debug.Log("bEffectSound : " + flag);
    }
    }
