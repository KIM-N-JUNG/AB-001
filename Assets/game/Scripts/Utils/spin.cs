using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float x = 0.0f;
    public float y = 0.0f;
    public float z = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(rotate());
    }

    IEnumerator rotate()
    {
        transform.Rotate(x, y, z);
        yield return new WaitForFixedUpdate();
    }
}
