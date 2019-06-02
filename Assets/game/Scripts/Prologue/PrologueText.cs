using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueText : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float speed = 0.01f;
    public float scrollPerSec = 0.01f;

    private float scrollPos = 1.0f;

    public delegate void OnFinishCb();
    public OnFinishCb onFinishCb { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROLOGUE)
        {
            replay();
            StartCoroutine(DoScrollText());
        }
    }

    private IEnumerator DoScrollText()
    {
        yield return new WaitForSeconds(1.0f);
        while (scrollPos > 0.01f /* end position */)
        {
            scrollPos -= speed;
            scrollbar.value = scrollPos;
            yield return new WaitForSeconds(scrollPerSec);
        }

        if (onFinishCb != null)
        {
            onFinishCb();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void replay()
    {
        scrollbar.value = 0.0f;
        scrollbar.value = 1.0f;
        scrollPos = 1.0f;
    }
}