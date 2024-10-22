using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHandler : MonoBehaviour
{
    public Material effectMaterial;
    private Camera camera;
    [Range(64.0f, 512.0f)] public float BlockCount = 128;
    private void Start()
    {
        camera = Camera.main;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        //camera.orthographicSize;
        float k = Camera.main.aspect;
        Vector2 count = new Vector2(BlockCount, BlockCount / k);
        Vector2 size = new Vector2(1.0f / count.x, 1.0f / count.y);
        //
        effectMaterial.SetVector("BlockCount", count);
        effectMaterial.SetVector("BlockSize", size);
        Graphics.Blit(source, destination, effectMaterial);
    }
}
