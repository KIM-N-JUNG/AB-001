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

        // offline Mode
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Offline Mode");
            return;
        }

        Debug.Log("InitializeGPGS");
        GPGSManager gpgsInstance = GPGSManager.GetInstance;

        // Login Callback
        gpgsInstance.Cb.onAuthenticationCb = (bool success, UserInfo userInfo) =>
        {
            Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + userInfo);
            //if (success)
            if (true)
            {
                // userId값으로 db에 query 
                userInfo.user_id = "asdfa";
                string query = "select * from user where user_id = " + "'" + userInfo.user_id +"'";

                MySqlConnector.Instance.DoSelectQuery(query, (MySqlDataReader reader) =>
                {
                    // 데이터 없음
                    if (reader == null)
                    {
                        // 프로필 입력 scene으로 이동
                        Debug.Log("There is no user -> newbie");
                        return;
                    }

                    /////////// for debuging ///////////
                    Debug.Log("Parsing data");
                    List<string> columns = GetDataReaderColumnNames(reader);
                    foreach (string col in columns)
                    {
                        Debug.Log(col);
                    }
                    /////////// for debuging ///////////

                    Debug.Log("reader: " + columns.ToString());
                    string nickName = reader["nick_name"].ToString();
                    int visitCount = int.Parse(reader["visit_count"].ToString());

                    Debug.Log(nickName + " 유저 방문 횟수 : " + (visitCount+1));
                    androidSet.ShowToast("환영합니다 " + nickName + "님. " + (visitCount+1) + "번째 방문입니다.", false);
                    int ret = MySqlConnector.Instance.DoNonQuery("update user set visit_count = " + (visitCount + 1) + " where `user_id` = " + "'" + userInfo.user_id + "'");
                    Debug.Log("ret is " + ret);
                });
            }
            else
            {
                androidSet.ShowToast("Login Failed", false);
            }
        };

        gpgsInstance.InitializeGPGS();
    }
}