using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        float time = timer.GetTime();
        int score = (int)(time * 150);

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, score);

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
        float time = timer.GetTime();
        int score = (int)(time * 150);

        GameObject obj = PauseUI.transform.Find("PauseText").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText("Game Over");

        GameObject btnResume = PauseUI.transform.Find("ResumeButton").gameObject;
        btnResume.SetActive(false);

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, score);

        paused = true;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        MainMenu();
        //Application.Quit();
    }
}
