using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuConstructor : MonoBehaviour
{
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(true);
        }
    }
}
