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

        User user = null;
        user = UserService.Instance.GetUserByUserNickname(nickName);
        if (user == null)
        {
            Debug.Log("사용 가능 한 닉네임(" + nickName + ") 입니다.");

            // user 등록
            user = new User
            {
                id = 0,
                country = "Korea",
                visit_count = 1,
                user_id = MainMenu.userInfo.user_id,
                email = MainMenu.userInfo.user_email,
                user_image = MainMenu.userInfo.user_image,
                user_name = MainMenu.userInfo.user_name,
                nick_name = nickName
            };

            Debug.Log("insert User");
            int r = UserService.Instance.InsertUser(user);
            Debug.Log("ret is " + r);
            if (r != 1)
            {
                androidSet.ShowToast("웁스!! 뭔가 문제가 생겼어요! 어쩌지??", false);
            }

            inputPanelUI.SetActive(false);
            imagePanelUI.SetActive(true);
            storyMessage.text = "인류의 미래는 " + nickName + " 자네의 손가락에 달렸네.. 행운을 비네!! (튜토리얼은 없다네)";
            Invoke("GoMainMenuScene", 5);
        } else
        {
            // 이미 있는 닉네임...
            Debug.Log("이미 있는 닉네임" + user.user_id);
            storyMessage.text = "다 아는 이름이구먼!";
            profileInput.indicateText.text = "이미 존재하는 닉네임 입니다 다시 입력하세요";
        }
    }
}

