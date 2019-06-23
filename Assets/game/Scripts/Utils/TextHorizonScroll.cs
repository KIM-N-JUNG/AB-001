using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextHorizonScroll : MonoBehaviour
{
	public Text textUI;
	public Scrollbar scrollbar;
	public float speed = 0.01f;
	public float scrollPerSec = 0.01f;
	public bool loop = true;
	public int SceneNumber = (int)Constant.SceneNumber.MAIN_MENU;

    public delegate void OnFinishCb();
    public OnFinishCb onFinishCb { get; set; }

	private float scrollPos = 0.0f;

	// Start is called before the first frame update
	void Start()
    {
		if (SceneManager.GetActiveScene().buildIndex == SceneNumber)
		{
            replay();
		}
	}

	private IEnumerator DoScrollText()
	{
        scrollbar.value = 0.0f;
        scrollPos = 0.0f;
        yield return new WaitForSeconds(0.5f);
		while (scrollPos < 1.1f /* end position */)
		{
			scrollPos += speed;
			scrollbar.value = scrollPos;
			yield return new WaitForSeconds(scrollPerSec);
		}

		if (onFinishCb != null)
		{
			onFinishCb();
		}
        yield return new WaitForSeconds(0.5f);
        if (loop)
		{
            StartCoroutine(DoScrollText());
        }
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void replay()
	{
        StartCoroutine(DoScrollText());
    }
}
