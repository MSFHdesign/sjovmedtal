using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    public bool ShouldItDrag; // Denne variabel kontrollerer, om objektet skal kunne trækkes

    void Update()
    {
        if (dragging && ShouldItDrag)
        {
            // Flyt objektet, idet den oprindelige offset tages i betragtning
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (ShouldItDrag) // Tjekker om objektet bør kunne trækkes
        {
            // Optag forskellen mellem objektets centrum og det klikkede punkt på kameraplanet
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
        }
    }

    private void OnMouseUp()
    {
        // Stop med at trække
        dragging = false;
    }
}
