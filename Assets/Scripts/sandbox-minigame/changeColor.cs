using UnityEngine;
using System.Collections.Generic;

public class ChangeColor : MonoBehaviour
{
    public List<Color> colorOptions = new List<Color>();
    public int colorIndex = 0;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && colorOptions.Count > 0)
        {
            colorIndex = Mathf.Clamp(colorIndex, 0, colorOptions.Count - 1);
            Color selectedColor = colorOptions[colorIndex];

            // Anvend den valgte farve til alle materialer på Mesh Renderer
            foreach (var material in renderer.materials)
            {
                material.color = selectedColor;
            }
        }
    }
}
