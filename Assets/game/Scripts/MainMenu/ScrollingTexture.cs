﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour
{
    public float scrollSpeed = 0.05f;
    public float scrollSpeed2 = 0.05f;
    Renderer rend;

    public Vector2 Scroll = new Vector2(0.05f, 0.05f);
    Vector2 Offset = new Vector2(0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1f;
        var offset = Time.time * scrollSpeed;
        var offset2 = Time.time * scrollSpeed2;
        if (offset > 1.0f) offset -= 1.0f;
        if (offset2 > 1.0f) offset2 -= 1.0f;

        rend.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(offset2, -offset));
    }

    void FixedUpdate()
    {

    }
}
