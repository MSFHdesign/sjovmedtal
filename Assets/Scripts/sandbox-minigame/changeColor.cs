using UnityEngine;
using System.Collections.Generic;

public class ChangeMaterial : MonoBehaviour
{
    public List<Material> materialOptions = new List<Material>();
    public int materialIndex = 0;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null && materialOptions.Count > 0)
        {
            materialIndex = Mathf.Clamp(materialIndex, 0, materialOptions.Count - 1);
            Material selectedMaterial = materialOptions[materialIndex];

            // Anvend det valgte materiale til Mesh Renderer
            renderer.material = selectedMaterial; // Skifter hele objektets materiale
        }
    }
}
