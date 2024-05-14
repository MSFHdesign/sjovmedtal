using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Drag dragScript = other.GetComponent<Drag>();
        if (dragScript != null)
        {
            dragScript.SetCanDrop(true);  // Fortæller Drag scriptet at det er over slettezonen
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Drag dragScript = other.GetComponent<Drag>();
        if (dragScript != null)
        {
            dragScript.SetCanDrop(false);  // Fortæller Drag scriptet at det ikke længere er over slettezonen
        }
    }
}
