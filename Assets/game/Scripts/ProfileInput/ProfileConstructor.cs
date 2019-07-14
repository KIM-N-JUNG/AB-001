using Ab001.Database.Dto;
using Ab001.Database.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileConstructor : MonoBehaviour
{
    public GameObject UIObject;
    

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)Constant.SceneNumber.PROFILE)
        {
            Debug.Log("Show Profile UI");
            UIObject.SetActive(true);
        }
        else
        {
            UIObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

