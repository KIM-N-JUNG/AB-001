using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonClass : MonoBehaviour
{
    static SingletonClass current = null;
    static GameObject container = null;

    public static SingletonClass Instance
    {
        get
        {
            if (current == null)
            {
                container = new GameObject();
                container.name = "Singleton";
                current = container.AddComponent(typeof(SingletonClass)) as SingletonClass;
                DontDestroyOnLoad(current);
            }
            return current;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    // ACCELERATION
    public bool acceleration = true;

    // 난이도
    public int level = 1;

    // 조이스틱 타입
    public int typeJoystick = 1;

    // 사운드
    public bool bBGSound = true;
    public bool bEffectSound = true;

    // 진동
    public bool bVibrate = true;

}
