using System.Collections;
using System.Collections.Generic;
using System.Data;
using Database.Dto;
using Database.Service;
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
            Debug.Log("onAuthenticationCb! - " + success + ", userInfo " + userInfo.user_name);
            if (success)
            {
                androidSet.ShowToast("Login Success", false);
                // userId값으로 db에 query 
                //userInfo.user_id = "asdf";
                string query = "select * from user where user_id = " + "'" + userInfo.user_id +"'";
                User user = null;
                user = UserService.Instance.GetUserByUserId(userInfo.user_id);
                if (user == null)
                {
                    androidSet.ShowToast("맙소사.. 유저 정보가 없습니다! 버그 입니다!", false);
                    return;
                }

                androidSet.ShowToast("환영합니다 " + user.user_name + "님. " + (user.visit_count + 1) + "번째 방문입니다.", false);
                int ret = UserService.Instance.UpdateUserById(user.id, "visit_count", user.visit_count + 1);
                Debug.Log("ret is " + ret);
            }
            else
            {
                androidSet.ShowToast("Login Failed", false);
            }
        };

        gpgsInstance.InitializeGPGS();
    }
}