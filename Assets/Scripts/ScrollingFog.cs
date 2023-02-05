using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingFog : MonoBehaviour
{
    public Vector2 ScrollingSpeed;

    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        meshRenderer.material.mainTextureOffset += ScrollingSpeed * Time.deltaTime;
    }
}
