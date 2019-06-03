using Database.Dto;
using Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileConstructor : MonoBehaviour
{
    public GameObject UIObject;
    public GameObject inputPanelUI;
    public GameObject imagePanelUI;
    public ProfileInput profileInput;
    public AndroidSet androidSet;
    public Text storyMessage;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROFILE)
        {
            Debug.Log("Show Profile UI");
            UIObject.SetActive(true);
            inputPanelUI.SetActive(true);
            imagePanelUI.SetActive(false);
        }
        else
        {
            UIObject.SetActive(false);
        }
    }

    // Update is called once per frame
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
        string nickName = profileInput.inputField.text;
        Debug.Log("닉네임 : " + nickName);

        if (nickName.Length <= 1)
        {
            storyMessage.text = "이름이 없나? 다시 물어보겠어";
            profileInput.indicateText.text = "2글자 이상 입력 해 주세요";
            return;
        }
        User user = null;
        user = UserService.Instance.GetUserByUserNickname(nickName);
        if (user == null)
        {
            Debug.Log("사용 가능 한 닉네임(" + nickName + ") 입니다.");

            if (MainMenu.userInfo.is_legacy_user == false)
            {
                // 새로운 user 등록
                user = new User
                {
                    country = MainMenu.userInfo.user_country,
                    visit_count = 1,
                    user_id = MainMenu.userInfo.user_id,
                    email = MainMenu.userInfo.user_email,
                    user_image = MainMenu.userInfo.user_image,
                    user_name = MainMenu.userInfo.user_name,
                    nick_name = nickName
                };

                Debug.Log("insert User");
                Debug.Log(user);
                int r = UserService.Instance.InsertUser(user);
                Debug.Log("ret is " + r);
                if (r != 1)
                {
                    Debug.Log("웁스!! 유저 등록 중 데이터베이스에 문제가 생겼어요! 어쩌지??");
                }
            }
            // 예전에 등록된 기존 유저
            else
            {
                // 닉네임만 업데이트 한다
                int r = UserService.Instance.UpdateUserByUserId(MainMenu.userInfo.user_id, "nick_name", nickName);
                if (r != 1)
                {
                    Debug.Log("웁스!! 유저 수정 중 데이터베이스에 문제가 생겼어요! 어쩌지??");
                }
            }

            inputPanelUI.SetActive(false);
            imagePanelUI.SetActive(true);
            storyMessage.text = "인류의 미래는 \"" + nickName + "\" 자네의 손가락에 달렸네.. 행운을 비네!! (튜토리얼은 없다네)";
            Invoke("GoMainMenuScene", 5);
        } else
        {
            // 이미 있는 닉네임...
            Debug.Log("이미 있는 닉네임" + user.user_id);
            Debug.Log(user);
            storyMessage.text = "다 아는 이름이구먼!";
            profileInput.indicateText.text = "이미 존재하는 닉네임 입니다 다시 입력하세요";
        }
    }
}

