using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueText : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float speed = 0.05f;
    private bool isShow = false;
    public delegate void OnFinishCb();
    public OnFinishCb onFinishCb { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Show(false);
        }
        else
        {
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
            if (this.onFinishCb != null)
            {
                this.onFinishCb();
            }
            return;
        }

        scrollbar.value -= (0.01f * speed);
    }

    public void Show(bool show)
    {
        isShow = show;
    }

    public void replay()
    {
        scrollbar.value = 1;
        Show(true);
    }
}