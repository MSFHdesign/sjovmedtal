using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    public bool ShouldItDrag; // Om objektet skal kunne trækkes
    private bool canDrop = false; // Om objektet kan droppes i zonen

    void Update()
    {
        if (dragging && ShouldItDrag)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (ShouldItDrag)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (canDrop)
        {
            Destroy(gameObject); // Destruér objektet hvis det slippes i zonen
        }
        dragging = false;
    }

    public void SetCanDrop(bool value)
    {
        canDrop = value;
    }
}
