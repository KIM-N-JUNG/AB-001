using System.Collections;
using System.Collections.Generic;
using Database.Dto;
using Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueText : MonoBehaviour
{
    public Text textUI;
    public Scrollbar scrollbar;
    public float speed = 0.01f;
    public float scrollPerSec = 0.01f;
    public AndroidSet androidSet;
    private float scrollPos = 1.0f;
    
    public delegate void OnFinishCb();
    public OnFinishCb onFinishCb { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            androidSet.ShowToast(Properties.GetIndicateOfflineModeMessage(), false);
            return;
        }

        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROLOGUE)
        {
            Prologue prologue = PrologueService.Instance.GetPrologueByContentTypeAndLanguage("prologue", (int)Application.systemLanguage);
            if (prologue == null)
            {
                Debug.Log("Failed to get prologue");
                return;
            }
            textUI.text = prologue.content;
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