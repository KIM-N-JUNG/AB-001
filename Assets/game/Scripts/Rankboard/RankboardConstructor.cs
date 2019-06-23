using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankboardConstructor : MonoBehaviour
{
    public GameObject ui;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("RankboardConstructor Awake");
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.RANK_BOARD)
        {
            ui.SetActive(true);
        }
    }

    void Update()
    {
#if UNITY_ANDROID
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.RANK_BOARD)
        {
            //if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    SceneManager.LoadScene((int)Constant.SceneNumber.MAIN_MENU);
                    return;
                }
            }
        }
#endif

    }
    void Start()
    {
        Debug.Log("RankboardConstructor Start");
    }
}
