using UnityEngine;

public class DeleteZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Something entered the trigger zone!");
        if (other.CompareTag("SpawnedObject"))
        {
            Destroy(other.gameObject);
        }
    }
}
