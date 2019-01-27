using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //GameObject toggleBackgroundSoundToggle;
    public Toggle toggleBackgroundSoundToggle;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        //toggleBackgroundSoundToggle = GameObject.Find("Toggle_backgroundSound");
        //if (SingletonClass.Instance.bBGSound)
        //{
        //    toggleBackgroundSoundToggle.GetComponent<Toggle>().isOn = false;
        //} else
        //{
        //    toggleBackgroundSoundToggle.GetComponent<Toggle>().isOn = true;
        //}
        //if (SingletonClass.Instance.bBGSound)
        //{
        //    toggleBackgroundSoundToggle.isOn = false;
        //}
        //else
        //{
        //    toggleBackgroundSoundToggle.isOn = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
