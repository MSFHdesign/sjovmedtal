using UnityEngine;

public class MeshSelector : MonoBehaviour
{
    public Mesh[] meshes;
    public int selectedMeshIndex = 0;
    private MeshFilter meshFilter;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
        selectedMeshIndex = Mathf.Clamp(selectedMeshIndex, 0, meshes.Length - 1);

        if (meshes.Length > 0)
        {
            meshFilter.mesh = meshes[selectedMeshIndex];
        }
    }

    public void SetMeshIndex(int index)
    {
        selectedMeshIndex = index;
        UpdateMesh();
    }

    // Optionally, if OnValidate is useful in the editor for debugging or testing
    void OnValidate()
    {
        UpdateMesh();
    }
}
