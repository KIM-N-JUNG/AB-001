using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitPopup : MonoBehaviour
{
    public GameObject exitPopupUI;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Properties.GetExitPopupMessage();   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
