using UnityEngine;

public class MeshSelector : MonoBehaviour
{
    public Mesh[] meshes;
    public QuestData.ShapeType selectedShapeType;
    private MeshFilter meshFilter;
    private ShapeComponent shapeComponent;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        shapeComponent = GetComponent<ShapeComponent>();
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
        if (shapeComponent == null)
        {
            shapeComponent = GetComponent<ShapeComponent>();
        }

        int index = (int)selectedShapeType;
        index = Mathf.Clamp(index, 0, meshes.Length - 1);

        if (meshes.Length > 0)
        {
            meshFilter.mesh = meshes[index];
        }

        // Opdater ShapeComponent baseret på selectedShapeType
        if (shapeComponent != null)
        {
            shapeComponent.SetShapeType(selectedShapeType);
        }
    }

    public void SetShapeType(QuestData.ShapeType shapeType)
    {
        selectedShapeType = shapeType;
        UpdateMesh();
    }

    // Optionally, if OnValidate is useful in the editor for debugging or testing
    void OnValidate()
    {
        UpdateMesh();
    }
}
