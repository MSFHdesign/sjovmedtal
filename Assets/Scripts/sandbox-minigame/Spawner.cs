using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Træk din prefab herind i inspector
    public int indexSpawner = 0;
    public Button spawnButton;

    void Start()
    {
        if (spawnButton != null)
        {
            spawnButton.onClick.AddListener(SpawnObject);
        }
        else
        {
            Debug.LogError("Spawn button not assigned in the Inspector");
        }
    }

    public void SpawnObject()
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        spawnedObject.tag = "SpawnedObject"; // Sæt tagget

        SpriteSelector spriteSelector = spawnedObject.GetComponentInChildren<SpriteSelector>();
        if (spriteSelector != null)
        {
            spriteSelector.SetSpriteIndex(indexSpawner); // Sæt den rigtige sprite baseret på indexSpawner
        }

        // Efter et objekt er oprettet, tjek for quest completion
        QuestManager.Instance.CheckShapes();
    }
}
