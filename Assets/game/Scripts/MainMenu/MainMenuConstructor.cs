using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    static List<string> GetDataReaderColumnNames(IDataReader rdr)
    {
        var columnNames = new List<string>();
        for (int i = 0; i < rdr.FieldCount; i++)
            columnNames.Add(rdr.GetName(i));
        return columnNames;
    }

    void Start()
    {
        Debug.Log("MainMenuConstructor Start");

        // disconnected
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Disconnected internet");
            return;
        }



        Debug.Log("InitializeGPGS");
        GPGSManager gpgsInstance = GPGSManager.GetInstance;

        gpgsInstance.Cb.onAuthenticationCb = (bool success, UserInfo userInfo) =>
        {
            Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + userInfo);
            //if (success)
            if (true)
            {
                // userId값으로 db에 query 
                userInfo.user_id = "asdf";
                string query = "select * from user where user_id = " + "'" + userInfo.user_id +"'";

                MySqlConnector.Instance.DoSelectQuery(query, (MySqlDataReader reader) =>
                {
                    Debug.Log("Parsing data");
                    List<string> columns = GetDataReaderColumnNames(reader);
                    foreach (string col in columns)
                    {
                        Debug.Log(col);
                    }
                    Debug.Log("reader: " + columns.ToString());
                    string nickName = reader["nick_name"].ToString();
                    int visitCount = int.Parse(reader["visit_count"].ToString());
                    if (nickName.Length == 0)
                    {
                        androidSet.ShowToast("NO NICKNAME", false);

                        // 프로필 입력 scene으로 이동
                    }
                    else
                    {
                        Debug.Log(nickName + " 유저 방문 횟수 : " + (visitCount+1));
                        androidSet.ShowToast(nickName + " 유저 방문 횟수 : " + (visitCount+1), false);
                        int ret = MySqlConnector.Instance.DoNonQuery("update user set visit_count = " + (visitCount + 1) + " where `user_id` = " + "'" + userInfo.user_id + "'");
                        Debug.Log("ret is " + ret);
                    }
                });

                /*
                MySqlConnector.Instance.DoSelectQuery("select * from `user`", (MySqlDataReader reader) =>
                {
                    //data 파싱
                    string temp = "nick_name: " + reader["nick_name"].ToString();
                    temp += "\nemail: " + reader["email"].ToString();
                    temp += "\ncountry: " + reader["country"].ToString();
                    androidSet.ShowToast("Login Success", false);
                    Debug.Log(temp);
                });
                */
            }
            else
            {
                androidSet.ShowToast("Login Failed", false);
            }
        };

        gpgsInstance.InitializeGPGS();
    }
}