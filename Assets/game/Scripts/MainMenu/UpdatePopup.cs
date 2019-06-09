using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdatePopup : MonoBehaviour
{
    public GameObject updatePopupUI;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Properties.GetUpdatePopupMessage();   
    }

    // Update is called once per frame
    void Update()
    {
    }
}
