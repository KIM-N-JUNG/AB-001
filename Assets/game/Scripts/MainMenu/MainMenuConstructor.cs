using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuConstructor : MonoBehaviour
{
    public GameObject mainMenu;
    public AndroidSet androidSet;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("MainMenuConstructor Awake");

        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.MAIN_MENU)
        {
            mainMenu.SetActive(true);

        }
    }

    void Start()
    {
        Debug.Log("MainMenuConstructor Start");

        Debug.Log("InitializeGPGS");
        GPGSManager gpgsInstance = GPGSManager.GetInstance;

        gpgsInstance.Cb.onAuthenticationCb = (bool success) =>
        {
            MySqlConnector.Instance.DoQuery("select * from `user`", (MySqlDataReader reader) =>
            {
                //data 파싱
                string temp = "nick_name: " + reader["nick_name"].ToString();
                temp += "\nemail: " + reader["email"].ToString();
                temp += "\ncountry: " + reader["country"].ToString();
                androidSet.ShowToast(temp, false);
            });
        };

        gpgsInstance.InitializeGPGS();
    }
}