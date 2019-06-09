using System.Collections;
using System.Collections.Generic;
using System.Data;
using Database.Dto;
using Database.Service;
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