using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{

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
}
