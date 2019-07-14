using System.Collections;
using System.Collections.Generic;
using System.Data;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuConstructor : MonoBehaviour
{
    public GameObject mainMenuUI;
    public Text versionText;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Locale Information ");
        Debug.Log("Application.systemLanguage: " + Application.systemLanguage);
        Debug.Log("System.Globalization.RegionInfo.CurrentRegion: " + System.Globalization.RegionInfo.CurrentRegion);

#if UNITY_ANDROID
        string localeVal = null;
        Debug.Log("Android native");
        using(AndroidJavaClass cls = new AndroidJavaClass("java.util.Locale")) {
            if (cls != null) {
                using (AndroidJavaObject locale = cls.CallStatic<AndroidJavaObject>("getDefault")) {
                    if (locale != null) {
                        localeVal = locale.Call<string>("getLanguage") + "_" + locale.Call<string>("getCountry");
                        Debug.Log("Android localeVal: " + localeVal);
                        Debug.Log("Android Language: " + locale.Call<string>("getLanguage"));
                        Debug.Log("Android Country: " + locale.Call<string>("getCountry"));
                    } else {
                        Debug.Log("locale null");
                    }
                }
            } else {
                Debug.Log("cls null");
            }
        }
#endif


        Debug.Log("MainMenuConstructor Awake");
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.MAIN_MENU)
        {
            mainMenuUI.SetActive(true);
        }
    }

    void Start()
    {
        Debug.Log("MainMenuConstructor Start");
    }
}