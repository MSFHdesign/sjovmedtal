using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnedObject"))
        {
            TouchManager touchManager = FindObjectOfType<TouchManager>();
            if (touchManager != null)
            {
                touchManager.SetObjectInDeleteZone(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpawnedObject"))
        {
            TouchManager touchManager = FindObjectOfType<TouchManager>();
            if (touchManager != null)
            {
                touchManager.SetObjectInDeleteZone(false);
            }
        }
    }
}
