using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private Sequence sequence;
    private QuestData[] incompleteQuests;
    private int currentQuestIndex = 0;

    public string titleName;
    public float wordsPerSecond = 3;
    public float minWaitTime = 2;
    public string questPath = "QuestData/Shapes"; // Standard sti til quests

    void Start()
    {
        UIManager.Instance.showGamePanel();

        // Indlæs quests fra den angivne sti
        QuestManager.Instance.LoadQuestsFromPath(questPath);

        // Indlæser alle ikke-fuldførte quest
        incompleteQuests = QuestManager.Instance.GetIncompleteQuests();
        if (incompleteQuests.Length > 0)
        {
            StartNextQuest();
        }
        else
        {
            Debug.LogError("No incomplete quests found.");
        }

        string sceneName = SceneManager.GetActiveScene().name;
        UIManager.Instance.ShowDeleteZone();
        if (string.IsNullOrEmpty(titleName))
        {
            UIManager.Instance.showTitle(sceneName);
        }
        else
        {
            UIManager.Instance.showTitle(titleName);
        }

        sequence = DialogManager.Instance.GetSequence(sceneName);
        if (sequence != null)
        {
            StartCoroutine(DisplayDialogs());
        }
        else
        {
            Debug.LogError("Sequence not found for the scene: " + sceneName);
        }
    }

    void StartNextQuest()
    {
        if (currentQuestIndex < incompleteQuests.Length)
        {
            QuestData currentQuest = incompleteQuests[currentQuestIndex];
            Debug.Log("SG: Incomplete quest loaded: " + currentQuest.questName);
            UIManager.Instance.ShowReferenceSprite(currentQuest.winningReference); // Vis reference-sprite

            bool questCompleted = QuestManager.Instance.CheckQuestCompletionByName(currentQuest.questName);

            if (!questCompleted)
            {
                StartCoroutine(DisplayQuestDescription(currentQuest));
            }
        }
        else
        {
            Debug.Log("SG: All quests completed.");
        }
    }

    IEnumerator DisplayDialogs()
    {
        foreach (var dialogue in sequence.dialog)
        {
            UIManager.Instance.showDialog(dialogue);

            float words = dialogue.Split(' ').Length;
            float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime);
            yield return new WaitForSeconds(dynamicWaitTime);
        }

        yield return new WaitForSeconds(1f);

        UIManager.Instance.hideDialog();
    }

    IEnumerator DisplayQuestDescription(QuestData quest)
    {
        yield return new WaitForSeconds(1f);

        if (quest != null && quest.winningReference != null)
        {
            UIManager.Instance.ShowReferenceSprite(quest.winningReference);
        }

        // Opdater score
        UIManager.Instance.UpdateScore(quest.currentAmount, quest.requiredAmount);

        // Vent lidt, før du starter næste quest
        yield return new WaitForSeconds(2f);

        currentQuestIndex++;
        StartNextQuest();
    }
}
