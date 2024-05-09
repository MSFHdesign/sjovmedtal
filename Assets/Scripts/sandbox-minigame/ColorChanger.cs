using UnityEngine;

public enum MeshColor
{
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Black
}

public class ColorChanger : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public MeshColor currentColor; // Offentlig variabel for at sætte farve fra inspector eller andet script

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        UpdateColor();
    }

    public void SetColor(MeshColor newColor)
    {
        currentColor = newColor;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (meshRenderer != null)
        {
            Color color = Color.white; // Standardfarve
            switch (currentColor)
            {
                case MeshColor.Red:
                    color = Color.red;
                    break;
                case MeshColor.Green:
                    color = Color.green;
                    break;
                case MeshColor.Blue:
                    color = Color.blue;
                    break;
                case MeshColor.Yellow:
                    color = Color.yellow;
                    break;
                case MeshColor.White:
                    color = Color.white;
                    break;
                case MeshColor.Black:
                    color = Color.black;
                    break;
            }

            // Ændrer farven på alle materialer anvendt til denne mesh
            foreach (var material in meshRenderer.materials)
            {
                material.color = color;
            }
        }
        else
        {
            Debug.LogError("MeshRenderer component not found on " + gameObject.name);
        }
    }
}
