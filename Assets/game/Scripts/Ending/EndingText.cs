using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingText : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float speed = 0.5f;
    private float scrollPos = 1.0f;
    private bool isShow = false;
    public delegate void OnFinishCb();
    public OnFinishCb onFinishCb { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.ENDING)
        {
            Show(false);
        }
        else
        {
            Debug.Log("Start()");
            replay();
            Show(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow == false)
        {
            return;
        }

        if (scrollbar.value <= 0.0f)
        {
            // ?? 저절로 0이 되는 경우가 생김.. ㅡㅡ
            return;
        }
        if (scrollPos <= 0.01f)
        {
            if (this.onFinishCb != null)
            {
                this.onFinishCb();
            }
            return;
        }
        scrollbar.value -= (float)(0.01f * speed);
        scrollPos -= (float)(0.01f * speed);
    }

    public void Show(bool show)
    {
        isShow = show;
        scrollbar.value = 0.0f;
        scrollbar.value = 1.0f;
        scrollPos = 1.0f;
    }

    public void replay()
    {
        Show(true);
    }
}