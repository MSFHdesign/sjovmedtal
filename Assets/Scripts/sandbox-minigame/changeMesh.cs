using UnityEngine;

public class MeshSelector : MonoBehaviour
{
    public Mesh[] meshes;
    public QuestData.ShapeType[] shapeTypes;
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
            SetShapeType();
        }
    }

    public void SetMeshIndex(int index)
    {
        selectedMeshIndex = index;
        UpdateMesh();
    }

    public void SetShapeType()
    {
        ShapeComponent shapeComponent = GetComponent<ShapeComponent>();
        if (shapeComponent != null && shapeTypes.Length > selectedMeshIndex)
        {
            shapeComponent.shapeType = shapeTypes[selectedMeshIndex];
            Debug.Log($"MeshSelector: Set shape type to {shapeComponent.shapeType}");
        }
    }

    void OnValidate()
    {
        UpdateMesh();
    }
}
