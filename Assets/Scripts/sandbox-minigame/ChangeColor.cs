using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Public material to be assigned in the inspector
    public Material unlitMaterial;

    // Reference to the mesh renderer
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();

        // Clone the material to ensure we are not changing the global material
        if (unlitMaterial != null)
        {
            meshRenderer.material = new Material(unlitMaterial);
        }
        else
        {
            Debug.LogError("Unlit material is not assigned.");
        }
    }

    // Method to change the color of the material
    public void ChangeColor(Color newColor)
    {
        if (meshRenderer != null && meshRenderer.material != null)
        {
            meshRenderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("MeshRenderer or material is missing.");
        }
    }

    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a ColorCarrier component
        ColorCarrier colorCarrier = collision.gameObject.GetComponent<ColorCarrier>();
        if (colorCarrier != null)
        {
            // Change the color to the color of the ColorCarrier
            ChangeColor(colorCarrier.carrierColor);
        }
    }
}
