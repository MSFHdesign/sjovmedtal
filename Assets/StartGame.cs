using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // For at bruge SceneManager

public class StartGame : MonoBehaviour
{
    private Sequence sequence; // Holder den sekvens, der hentes fra DialogManager
    public Quest startingQuest;

    public string titleName; // optional titel, ellers vises scenens navn fra lib.
    public float wordsPerSecond = 3; // Hvor mange ord der vises pr. sekund
    public float minWaitTime = 2; // Minimum ventetid
    public string getQuest;

    void Start()
    {
        UIManager.Instance.showGamePanel();

        // Indlæser og starter en specifik quest
        startingQuest = QuestManager.Instance.GetQuest(getQuest);
        if (startingQuest != null)
        {
            Debug.Log("SG: Quest loaded: " + startingQuest.QuestName);
            bool questCompleted = QuestManager.Instance.CheckQuestCompletionByName(getQuest);
         
            if (!questCompleted && startingQuest.Description != null && startingQuest.Description.Length > 0)
            {
                StartCoroutine(DisplayQuestDescription());
        
            }
        }
        else
        {
            Debug.LogError("Quest not found: " + getQuest);
        }

        // Bruger scenens navn til at hente den tilsvarende Sequence
        string sceneName = SceneManager.GetActiveScene().name;
        UIManager.Instance.ShowDeleteZone(); // Viser slet knappen
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
            Debug.LogError("Sequence not found for the scene: " + sceneName);
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

        // Vent lidt ekstra, før du viser questbeskrivelserne
        yield return new WaitForSeconds(1f);

        // Vis questbeskrivelser, hvis der er nogen
        if (startingQuest != null && startingQuest.Description != null && startingQuest.Description.Length > 0)
        {
            foreach (var description in startingQuest.Description)
            {
                UIManager.Instance.showDialog(description);

               
                float words = description.Split(' ').Length; 
                float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime);
                yield return new WaitForSeconds(dynamicWaitTime); 
            }
        }

        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }

    IEnumerator DisplayQuestDescription()
    {
        // Vent lidt, før du viser questbeskrivelserne
        yield return new WaitForSeconds(1f);

        // Vis questbeskrivelser, hvis der er nogen
        if (startingQuest != null && startingQuest.Description != null && startingQuest.Description.Length > 0)
        {
            foreach (var description in startingQuest.Description)
            {
                UIManager.Instance.showDialog(description);

                // Beregner ventetiden baseret på antal ord i beskrivelsen
                float words = description.Split(' ').Length; // Antager at ord er adskilt af mellemrum
                float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime); // Sørger for at ventetiden aldrig er mindre end minWaitTime
                yield return new WaitForSeconds(dynamicWaitTime); // Venter dynamisk tid baseret på beskrivelsens længde
            }
        }

        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }
}
