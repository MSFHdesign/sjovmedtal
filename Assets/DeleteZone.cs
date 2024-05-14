using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Drag dragScript = other.GetComponent<Drag>();
        if (dragScript != null)
        {
            dragScript.SetCanDrop(true);  // Fort�ller Drag scriptet at det er over slettezonen
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Drag dragScript = other.GetComponent<Drag>();
        if (dragScript != null)
        {
            dragScript.SetCanDrop(false);  // Fort�ller Drag scriptet at det ikke l�ngere er over slettezonen
        }
    }
}
