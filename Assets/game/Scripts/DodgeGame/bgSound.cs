using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgSound : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource myAudio;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bool flag = SingletonClass.Instance.bBGSound;

        if (SceneManager.GetActiveScene().buildIndex == 1 
            && flag)
        {
            myAudio = GetComponent<AudioSource>();
            myAudio.clip = sound;
            myAudio.Play();
        }
    }

    private void OnApplicationQuit()
    {
        bool flag = SingletonClass.Instance.bBGSound;

        if (flag)
        {
            myAudio = GetComponent<AudioSource>();
            myAudio.Stop();

            SingletonClass.Instance.bBGSound = false;
        }
    }
}