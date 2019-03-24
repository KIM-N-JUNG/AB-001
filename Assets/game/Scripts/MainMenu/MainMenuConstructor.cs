using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuConstructor : MonoBehaviour
{
    public GameObject mainMenu;
    public Camera camera;
    public Matrix4x4 originalProjection;

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMenu.SetActive(true);
            Matrix4x4 p = originalProjection;
            p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.1F;
            p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.1F;
            camera.projectionMatrix = p;
        }
    }
}
