using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseUI;
    public Timer timer;
    public Score score;
    public Text PanelText;
    public GameObject starField;
    public GameObject plane;

    private bool paused = false;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        PauseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            return;
        }

        if (paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        } else 
        {
            PauseUI.SetActive(false);
            //Time.timeScale = 1f;

            int onControl = PlayerPrefs.GetInt("onControl");
            if (onControl == 1)
            {
                Time.timeScale = 1f;
            } else
            {
                Time.timeScale = 0.3f;
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");

        timer.Pause();
        score.Pause();

        float time = timer.GetTime();
        int s = score.GetScore();

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, s);

        paused = true;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        paused = false;
        timer.Resume();
        score.Resume();
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
        int s = score.GetScore();

        GameObject obj = PauseUI.transform.Find("PauseText").gameObject;
        TextMeshProUGUI pauseText = obj.GetComponent<TextMeshProUGUI>();
        pauseText.SetText("Game Over");

        GameObject btnResume = PauseUI.transform.Find("ResumeButton").gameObject;
        btnResume.SetActive(false);

        GameObject meshText = PauseUI.transform.Find("ScoreMeshText").gameObject;
        TextMeshProUGUI text = meshText.GetComponent<TextMeshProUGUI>();
        text.SetText("Time: {0:2} \r\nScore: {1:0}", time, s);

        paused = true;

        GPGSManager.GetInstance.SubmitToLeaderBorad(s);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        paused = false;
        starField.SetActive(false);
        plane.SetActive(false);
        MainMenu();
    }
}
