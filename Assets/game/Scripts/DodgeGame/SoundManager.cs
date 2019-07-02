using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        bool flag = SingletonClass.Instance.bEffectSound;

        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.GAME && flag)
        {
            myAudio.PlayOneShot(soundExplosion);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
