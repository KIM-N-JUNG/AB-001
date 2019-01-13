using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip soundExplosion;
    private AudioSource myAudio;

    public static SoundManager instance;

    void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    public void playSound()
    {
        myAudio.PlayOneShot(soundExplosion);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
