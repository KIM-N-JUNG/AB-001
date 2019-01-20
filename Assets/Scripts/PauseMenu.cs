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
        Debug.Log("Pause");
        paused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        paused = false;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene("AvoidBullets");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene(0);
    }

    public void End()
    {
        paused = true;

        float time = timer.GetTime();
        float t = Mathf.Round(time / .01f) * .01f;
        PanelText.text = "Game Over \r\n\r\n Your Time : " + t + "'";
    }

    public void Quit()
    {
        Debug.Log("Quit");
        MainMenu();
        //Application.Quit();
    }
}
