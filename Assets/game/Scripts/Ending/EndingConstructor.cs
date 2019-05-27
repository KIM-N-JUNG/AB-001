using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingConstructor : MonoBehaviour
{
    public GameObject UIObject;
    public EndingText endingText;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.ENDING)
        {
            Debug.Log("Show Ending UI");
            UIObject.SetActive(true);

            endingText.onFinishCb = () =>
            {
                HandleOnFinishCb();
            };
            endingText.replay();
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

    private void HandleOnFinishCb()
    {
        Debug.Log("HandleOnFinishCb()");
        SceneManager.LoadScene((int)Constant.SceneNumber.MAIN_MENU);
    }

    public void Skip()
    {
        Debug.Log("Skip()");
        HandleOnFinishCb();
    }
}
