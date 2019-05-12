using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConstructor : MonoBehaviour
{
    public GameObject ui;
    public GameObject shuttle;
    public GameObject starField;
    public GameObject plane;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.GAME)
        {
            ui.SetActive(true);
            shuttle.SetActive(true);
            starField.SetActive(true);
            plane.SetActive(true);
        }
    }
}
