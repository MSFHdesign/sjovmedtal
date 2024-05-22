using UnityEngine;

public class ShapeComponent : MonoBehaviour
{
    public QuestData.ShapeType shapeType;

    public void SetShapeType(QuestData.ShapeType type)
    {
        shapeType = type;
    }
}
