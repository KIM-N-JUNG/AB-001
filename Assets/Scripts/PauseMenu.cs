using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    public Timer timer;
    public Text PanelText;

    private bool paused = false;

	// Use this for initialization
	void Start () {
        PauseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        } else 
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("AvoidBullets");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void End()
    {
        paused = true;

        float time = timer.GetTime();
        float t = Mathf.Round(time / .01f) * .01f;
        PanelText.text = "Game Over \r\n\r\n Your Time : " + t;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
