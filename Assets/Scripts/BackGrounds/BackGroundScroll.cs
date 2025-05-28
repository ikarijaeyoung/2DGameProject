using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField]
    private Renderer bgRenderer;

    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
