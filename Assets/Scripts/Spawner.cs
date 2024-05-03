using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Træk din prefab herind i inspector
    public int indexSpawner = 0;
    public Button spawnButton;

    void Start()
    {
        spawnButton.onClick.AddListener(SpawnObject);
    }

    public void SpawnObject()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        MeshSelector meshSelector = spawnedObject.GetComponentInChildren<MeshSelector>();
        if (meshSelector != null)
        {
            meshSelector.SetMeshIndex(indexSpawner);
        }
    }
}
