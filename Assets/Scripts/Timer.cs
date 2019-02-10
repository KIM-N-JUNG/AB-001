using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static float time;
    public string status; // play, pause, end 

    private Text uiText;

    // Use this for initialization
    void Start()
    {
        uiText = GetComponent<Text>();

        time = 0.00f;
        uiText.text = "Time : " + time.ToString();

        status = "play";
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "play")
        {
            time += Time.deltaTime;
        }
        else if (status == "pause")
        {
            return;
        }
        else
        {
            return;
        }

        float t = (float)System.Math.Truncate(time * 100.0f) / 100.0f;
        uiText.text = "Time : " + t.ToString();
    }

    public float GetTime()
    {
        return time;
    }
}