using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Inkluder denne for at bruge SceneManager

public class StartGame : MonoBehaviour
{
    private Sequence sequence; // Holder den sekvens, der hentes fra DialogManager
    public int awaitTime = 20; // Standard værdi sat, kan overskrives i Inspector
    public string titleName;

    void Start()
    {
        UIManager.Instance.showGamePanel();
        // Bruger scenens navn til at hente den tilsvarende Sequence
        string sceneName = SceneManager.GetActiveScene().name;

        // Viser titlen baseret på om titleName er sat eller ej
        if (string.IsNullOrEmpty(titleName))
        {
            UIManager.Instance.showTitle(sceneName);
        }
        else
        {
            UIManager.Instance.showTitle(titleName);
        }

        sequence = DialogManager.Instance.GetSequence(sceneName);
        if (sequence != null) // Sikrer, at sekvensen er hentet korrekt
        {
            StartCoroutine(DisplayDialogs());
        }
        else
        {
            Debug.LogError("Sekvens ikke fundet for scenen: " + sceneName);
        }
    }

    IEnumerator DisplayDialogs()
    {
        foreach (var dialogue in sequence.dialog)
        {
            UIManager.Instance.showDialog(dialogue);
            yield return new WaitForSeconds(awaitTime); // Venter angivet tid mellem hver dialog
        }
        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }
}
