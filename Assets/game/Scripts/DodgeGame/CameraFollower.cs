using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;
    public float shakeTimer;
    public float shakeAmount;
    public bool followAllowed;
    public GameObject shuttle;

    private void FixedUpdate()
    {
        if (followAllowed == false)
            return;

        float posX = Mathf.SmoothDamp(transform.position.x, shuttle.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, shuttle.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    void Start()
    {
        followAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer >= 0.0f)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }
}
