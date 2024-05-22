using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ColorFilter : MonoBehaviour
{
    private Material material;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/ColorFilter"));
    }

    public void SetColor(Color color)
    {
        if (material != null)
        {
            material.SetColor("_Color", color);
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}

