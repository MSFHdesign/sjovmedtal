using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered trigger with: {other.gameObject.name}");
        if (other.CompareTag("SpawnedObject"))
        {
            Debug.Log("Object is a SpawnedObject, preparing to set delete zone flag.");

            // Check for MouseManager first
            MouseManager mouseManager = FindObjectOfType<MouseManager>();
            if (mouseManager != null && mouseManager.isObjectSelected)
            {
                mouseManager.SetObjectInDeleteZone(true);
                Debug.Log("MouseManager found and delete zone flag set.");
            }
            else
            {
                // If MouseManager is not found or not active, check for TouchManager
                TouchManager touchManager = FindObjectOfType<TouchManager>();
                if (touchManager != null)
                {
                    touchManager.SetObjectInDeleteZone(true);
                    Debug.Log("TouchManager found and delete zone flag set.");
                }
                else
                {
                    Debug.LogWarning("Neither MouseManager nor TouchManager found.");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Exited trigger with: {other.gameObject.name}");
        if (other.CompareTag("SpawnedObject"))
        {
            // Check for MouseManager first
            MouseManager mouseManager = FindObjectOfType<MouseManager>();
            if (mouseManager != null && mouseManager.isObjectSelected)
            {
                mouseManager.SetObjectInDeleteZone(false);
                Debug.Log("MouseManager delete zone flag cleared.");
            }
            else
            {
                // If MouseManager is not found or not active, check for TouchManager
                TouchManager touchManager = FindObjectOfType<TouchManager>();
                if (touchManager != null)
                {
                    touchManager.SetObjectInDeleteZone(false);
                    Debug.Log("TouchManager delete zone flag cleared.");
                }
                else
                {
                    Debug.LogWarning("Neither MouseManager nor TouchManager found.");
                }
            }
        }
    }
}
