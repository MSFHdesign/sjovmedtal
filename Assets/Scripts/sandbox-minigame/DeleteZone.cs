using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered trigger with: {other.gameObject.name}");
        if (other.CompareTag("SpawnedObject"))
        {
            Debug.Log("Object is a SpawnedObject, preparing to delete.");
            // Slet objektet når det trækkes ind i sletteområdet
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited trigger with: {other.gameObject.name}");
    }
}
