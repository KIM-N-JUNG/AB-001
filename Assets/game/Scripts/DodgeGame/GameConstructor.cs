using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConstructor : MonoBehaviour
{
    public GameObject ui;
    public GameObject shuttle;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            ui.SetActive(true);
            shuttle.SetActive(true);
        }
    }
}
