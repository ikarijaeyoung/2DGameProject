using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    [SerializeField]
    private Renderer bgRenderer;
    public static bool isScrolling = true;

    private void Start()
    {
        bgRenderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        if (isScrolling)
        {
            ScrollBackground();
        }
    }
    private void ScrollBackground()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }
}
