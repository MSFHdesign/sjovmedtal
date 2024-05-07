using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Inkluder denne for at bruge SceneManager

public class test : MonoBehaviour
{
    private Sequence sequence; // Holder den sekvens, der hentes fra DialogManager
    public int awaitTime;
    void Start()
    {
        UIManager.Instance.showGamePanel();
        // Bruger scenens navn til at hente den tilsvarende Sequence
        string sceneName = SceneManager.GetActiveScene().name;
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
            yield return new WaitForSeconds(awaitTime); // Vent 20 sekunder mellem hver dialog
        }
        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }
}
