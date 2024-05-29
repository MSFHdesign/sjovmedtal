using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    public Sprite[] sprites;
    public QuestData.ShapeType[] shapeTypes; // Beholder denne linje
    public int selectedSpriteIndex = 0;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        selectedSpriteIndex = Mathf.Clamp(selectedSpriteIndex, 0, sprites.Length - 1);

        if (sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[selectedSpriteIndex];
            SetShapeType(); // Beholder denne linje
        }
    }

    public void SetSpriteIndex(int index)
    {
        selectedSpriteIndex = index;
        UpdateSprite();
    }

    public void SetShapeType() // Beholder denne metode som public
    {
        ShapeComponent shapeComponent = GetComponent<ShapeComponent>();
        if (shapeComponent != null && shapeTypes.Length > selectedSpriteIndex)
        {
            shapeComponent.shapeType = shapeTypes[selectedSpriteIndex];
            Debug.Log($"SpriteSelector: Set shape type to {shapeComponent.shapeType}");
        }
    }

    void OnValidate()
    {
        UpdateSprite();
    }
}
