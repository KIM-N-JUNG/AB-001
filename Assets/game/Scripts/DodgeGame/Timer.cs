﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static float time;
    private bool bPause; // play, pause, end 

    private Text uiText;

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
        time = 0.0f;
    }

    // Use this for initialization
    void Start()
    {
        uiText = GetComponent<Text>();

        time = 0.00f;
        uiText.text = "Time : " + time.ToString();
        bPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bPause == true)
        {
            return;
        }

        time += Time.deltaTime;

        float t = (float)System.Math.Truncate(time * 100.0f) / 100.0f;
        uiText.text = "Time : " + t.ToString();
    }

    public float GetTime()
    {
        return time;
    }
}