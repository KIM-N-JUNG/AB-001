using System;
using System.Collections;
using System.Collections.Generic;
using Ab001.Database.Dto;
using Ab001.Database.Service;
using Ab001.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileInput : MonoBehaviour
{
    public Text indicateText;
    public InputField inputField;
    public GameObject inputPanelUI;
    public GameObject imagePanelUI;
    public AndroidSet androidSet;
    public Text storyMessage;

    void Start()
    {
    }

    void Update()
    {
    }

    private void GoMainMenuScene()
    {
        SceneManager.LoadScene((int)Constant.SceneNumber.MAIN_MENU);
    }

    private void GoEndingScene()
    {
        SingletonClass.Instance.bLogin = false;
        SceneManager.LoadScene((int)Constant.SceneNumber.ENDING);
    }

    public void HandleCancel()
    {
        storyMessage.text = "젠장... 우린 이제 끝장이군!!";

        Invoke("GoEndingScene", 2);
    }

    public void HandleOK()
    {
        string nickName = inputField.text;
        Debug.Log("닉네임 : " + nickName);

        if (nickName.Length <= 1)
        {
            storyMessage.text = "이름이 없나? 다시 물어보겠어";
            indicateText.text = "2글자 이상 입력 해 주세요";
            return;
        }

        User user = null;
        R_UserGame userGame = null;
        try
        {
            userGame = R_UserGameService.Instance.GetUserGameByNickNameAndGameCode(nickName, Constant.GAME_CODE);
            if (userGame != null)
            {
                // 이미 있는 닉네임...
                Debug.Log("이미 있는 닉네임" + userGame.user_id);
                storyMessage.text = "다 아는 이름이구먼!";
                indicateText.text = "이미 존재하는 닉네임 입니다 다시 입력하세요";
                return;
            }
            
            MainMenu.userInfo.nick_name = nickName;
            Debug.Log("사용 가능 한 닉네임(" + MainMenu.userInfo.nick_name + ") 입니다.");

            // 새로운 user 등록
            user = new User
            {
                auth = MainMenu.userInfo.auth,
                country = MainMenu.userInfo.user_country,
                visit_count = 1,
                user_id = MainMenu.userInfo.user_id,
                email = MainMenu.userInfo.user_email,
                user_image = MainMenu.userInfo.user_image,
                user_name = MainMenu.userInfo.user_name
            };

            int r = UserService.Instance.InsertUser(user);
            if (r != 1)
            {
                throw new InvalidOperationException("Failed to insert user record");
            }

            //  UserGame 등록
            R_UserGame newUserGame = new R_UserGame
            {
                user_id = user.user_id,
                game_code = Constant.GAME_CODE,
                nick_name = MainMenu.userInfo.nick_name,
                create_date = DateTimeManager.Instance.getKoreaTimeFromUTCNow()
            };
            r = R_UserGameService.Instance.InsertR_UserGame(newUserGame);
            if (r != 1)
            {
                throw new InvalidOperationException("Failed to insert userGame record");
            }

            inputPanelUI.SetActive(false);
            imagePanelUI.SetActive(true);
            storyMessage.text = "인류의 미래는 \"" + nickName + "\" 자네의 손가락에 달렸네.. 행운을 비네!! (튜토리얼은 없다네)";

            SingletonClass.Instance.bLogin = true;
            PlayerPrefs.SetInt("bLogin", SingletonClass.Instance.bLogin ? 1 : 0);
            PlayerPrefs.Save();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            storyMessage.text = "치지직.. 통신 불량.. 유저 등록 실패..";
            UserService.Instance.deleteUser(user);
        }
        finally
        {
            Invoke("GoMainMenuScene", 3);
        }
    }
}
