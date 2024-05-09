using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Inkluder denne for at bruge SceneManager

public class StartGame : MonoBehaviour
{
    private Sequence sequence; // Holder den sekvens, der hentes fra DialogManager
    
    public string titleName; // optional titel, ellers vises scenens navn fra lib.
    public float wordsPerSecond = 3; //  Hvor mange ord der vises pr. sekund
    public float minWaitTime = 2; // Minimum ventetid
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

            // Beregner ventetiden baseret på antal ord i dialogen
            float words = dialogue.Split(' ').Length; // Antager at ord er adskilt af mellemrum
          
            float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime); // Sørger for at ventetiden aldrig er mindre end minWaitTime

            yield return new WaitForSeconds(dynamicWaitTime); // Venter dynamisk tid baseret på dialogens længde

        }
        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }

}
