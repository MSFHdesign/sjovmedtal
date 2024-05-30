using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered trigger with: {other.gameObject.name}");
        if (other.CompareTag("SpawnedObject"))
        {
            Debug.Log("Object is a SpawnedObject, preparing to set delete zone flag.");
            TouchManager touchManager = FindObjectOfType<TouchManager>();
            if (touchManager != null)
            {
                touchManager.SetObjectInDeleteZone(true);
                Debug.Log("TouchManager found and delete zone flag set.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited trigger with: {other.gameObject.name}");
        if (other.CompareTag("SpawnedObject"))
        {
            TouchManager touchManager = FindObjectOfType<TouchManager>();
            if (touchManager != null)
            {
                touchManager.SetObjectInDeleteZone(false);
                Debug.Log("TouchManager delete zone flag cleared.");
            }
        }
    }
}
