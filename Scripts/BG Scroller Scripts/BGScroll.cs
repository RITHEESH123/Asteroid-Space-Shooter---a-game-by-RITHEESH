using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float scroll_Speed = 0.1f;

    private MeshRenderer mesh_Renderer;

    private float x_Scroll;

    void Awake()
    {
        mesh_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        x_Scroll = Time.time * scroll_Speed;
        Vector2 offset = new Vector2(x_Scroll, 0f);
        mesh_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
