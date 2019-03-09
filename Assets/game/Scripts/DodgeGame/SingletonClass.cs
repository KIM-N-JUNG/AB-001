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

    public void Awake()
    {
        // ACCELERATION
        if (PlayerPrefs.HasKey("acceleration"))
            acceleration = PlayerPrefs.GetInt("acceleration") == 1 ? true : false;

        // 난이도
        if (PlayerPrefs.HasKey("level"))
            level = PlayerPrefs.GetInt("level");

        // 조이스틱 타입
        if (PlayerPrefs.HasKey("joystick"))
            typeJoystick = PlayerPrefs.GetInt("joystick");

        // 사운드
        if (PlayerPrefs.HasKey("bgSound"))
            bBGSound = PlayerPrefs.GetInt("bgSound") == 1 ? true : false;
        if (PlayerPrefs.HasKey("effectSound"))
            bEffectSound = PlayerPrefs.GetInt("effectSound") == 1 ? true : false;

        // 진동
        if (PlayerPrefs.HasKey("vibrate"))
            bVibrate = PlayerPrefs.GetInt("vibrate") == 1 ? true : false;
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
    public int level = 0;

    // 조이스틱 타입
    public int typeJoystick = 1;

    // 사운드
    public bool bBGSound = true;
    public bool bEffectSound = true;

    // 진동
    public bool bVibrate = true;

}
