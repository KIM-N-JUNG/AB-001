using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileInput : MonoBehaviour
{
    public Text indicateText;
    public InputField inputField;
    public Text storyMessage;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive |= SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROFILE;
        Debug.Log("isActive : " + isActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
            return;
    }
}
