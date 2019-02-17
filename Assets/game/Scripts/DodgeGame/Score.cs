using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static readonly int BASIC_RATIO = 150;
    protected readonly float [] scoreRatios = {
        BASIC_RATIO * 0.7f, 
        BASIC_RATIO * 1.0f, 
        BASIC_RATIO * 1.3f,
        BASIC_RATIO * 2.0f
    };

    private bool bPause;
    private Text uiText;
    public Timer timer;
    public static int score;

    public void Pause()
    {
        bPause = true;
    }
    public void Resume()
    {
        bPause = false;
    }
    public void Reset()
    {
        score = 0;
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("Start Score");
        uiText = GetComponent<Text>();

        score = 0;
        uiText.text = "0";
        bPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bPause == true)
        {
            return;
        }

        var ins = SingletonClass.Instance;         
        float time = timer.GetTime();
        score = (int)(time * scoreRatios[ins.level]);

        uiText.text = "Score : " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}