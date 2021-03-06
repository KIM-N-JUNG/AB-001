﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueConstructor : MonoBehaviour
{
    public GameObject UIObject;
    public PrologueText prologueText;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROLOGUE)
        {
            Debug.Log("Show Prologue UI");
            UIObject.SetActive(true);

            prologueText.onFinishCb = () =>
            {
                HandleOnFinishCb();
            };

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
        if (SingletonClass.Instance.bRegisterProfile == false)
        {
            SceneManager.LoadScene((int)Constant.SceneNumber.PROFILE);
        }
        else
        {
            SceneManager.LoadScene((int)Constant.SceneNumber.MAIN_MENU);
        }
    }

    public void Skip()
    {
        Debug.Log("Skip()");
        HandleOnFinishCb();
    }
}
