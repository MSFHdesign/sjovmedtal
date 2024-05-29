using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    public Sprite[] sprites;
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
        }
    }

    public void SetSpriteIndex(int index)
    {
        selectedSpriteIndex = index;
        UpdateSprite();
    }

    void OnValidate()
    {
        UpdateSprite();
    }
}
