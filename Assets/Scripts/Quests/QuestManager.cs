using Thrakal;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class QuestManager : Singleton<QuestManager>
{
    public QuestData[] quests;
    private QuestData[] incompleteQuests;
    private int currentQuestIndex = 0;
    private bool questInProgress = false;

    public override void Awake()
    {
        base.Awake();
    }

    public void LoadQuestsFromPath(string path)
    {
        quests = Resources.LoadAll<QuestData>(path);
        incompleteQuests = GetIncompleteQuests();
        Debug.Log("QM: Number of incomplete quests found: " + incompleteQuests.Length);
    }

    public QuestData GetQuest(string questName)
    {
        foreach (QuestData quest in quests)
        {
            if (quest.questName == questName)
                return quest;
        }
        Debug.LogError("QM: Quest not found: " + questName);
        return null;
    }

    private QuestData[] GetIncompleteQuests()
    {
        QuestData[] incomplete = quests.Where(quest => !quest.isCompleted).ToArray();
        ShuffleQuests(incomplete); // Shuffle the quests
        return incomplete;
    }

    private void ShuffleQuests(QuestData[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            QuestData value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public void UpdateStatus(string questName, string status)
    {
        QuestData quest = GetQuest(questName);
        if (quest != null)
        {
            quest.status = status;
            Debug.Log($"QM: Quest '{questName}' status updated to: {status}");
        }
        else
        {
            Debug.LogError($"QM: Quest with name '{questName}' not found.");
        }
    }

    public bool CheckQuestCompletionByName(string questName)
    {
        QuestData quest = GetQuest(questName);

        if (quest == null)
        {
            Debug.LogError($"QM: Quest with name '{questName}' not found.");
            return false;
        }

        var playerShapes = FindObjectsOfType<ShapeComponent>().ToList();
        Debug.Log($"QM: Player shapes count: {playerShapes.Count}, Required shapes count: {quest.requiredShapes.Count}");

        foreach (var shape in playerShapes)
        {
            Debug.Log($"QM: Player shape found: {shape.shapeType}");
        }

        if (playerShapes.Count < quest.requiredShapes.Count)
        {
            Debug.Log("QM: Not enough shapes created by player.");
            quest.isCompleted = false;
            return false;
        }

        foreach (var requiredShape in quest.requiredShapes)
        {
            bool shapeFound = playerShapes.Any(shape => shape.shapeType == requiredShape.shapeType);
            Debug.Log($"QM: Checking for required shape: {requiredShape.shapeType}, Found: {shapeFound}");
            if (!shapeFound)
            {
                Debug.Log($"QM: Required shape '{requiredShape.shapeType}' not found among player's shapes.");
                quest.isCompleted = false;
                return false;
            }
        }

        quest.isCompleted = true;
        Debug.Log($"QM: Quest '{quest.questName}' completed.");
        return true;
    }

    public void MarkQuestAsCompleted(string questName)
    {
        QuestData quest = GetQuest(questName);
        if (quest != null)
        {
            quest.isCompleted = true;
            Debug.Log($"QM: Quest '{questName}' marked as completed.");
        }
    }

    public void UpdateCurrentAmount(string questName, int amount)
    {
        QuestData quest = GetQuest(questName);
        if (quest == null)
        {
            Debug.LogError($"Quest with name '{questName}' not found.");
            return;
        }

        quest.currentAmount += amount;
        Debug.Log($"Quest '{quest.questName}' updated: Current amount is now {quest.currentAmount}");

        if (quest.currentAmount >= quest.requiredAmount)
        {
            CheckQuestCompletionByName(questName);
        }
    }

    public void StartNextQuest()
    {
        if (questInProgress)
        {
            return;
        }

        if (currentQuestIndex < incompleteQuests.Length)
        {
            QuestData currentQuest = incompleteQuests[currentQuestIndex];
            Debug.Log("QM: Incomplete quest loaded: " + currentQuest.questName);
            UIManager.Instance.ShowReferenceSprite(currentQuest.winningReference);

            // Play the current quest's description along with sequences
            if (currentQuest.questDescription != null && currentQuest.questDescription.Count > 0)
            {
                Debug.Log("QM: Playing quest description for quest: " + currentQuest.questName);
                DialogManager.Instance.PlaySequencesAndQuestDescription(currentQuest.questDescription[0]);
            }

            bool questCompleted = CheckQuestCompletionByName(currentQuest.questName);
            Debug.Log("QM: Quest completion status: " + questCompleted);

            if (!questCompleted)
            {
                questInProgress = true;
                UIManager.Instance.UpdateScore(currentQuest.currentAmount, currentQuest.requiredAmount);
                Debug.Log("QM: Starting quest: " + currentQuest.questName);
            }
            else
            {
                Debug.Log("QM: Quest already completed: " + currentQuest.questName);
                currentQuestIndex++;
                StartNextQuest();
            }
        }
        else
        {
            Debug.Log("QM: All quests completed.");
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < incompleteQuests.Length)
        {
            QuestData currentQuest = incompleteQuests[currentQuestIndex];
            MarkQuestAsCompleted(currentQuest.questName);
            questInProgress = false;
            currentQuestIndex++;
            ClearAllShapes();

            // Start the next quest and play its description if available
            StartNextQuest();

            if (currentQuestIndex < incompleteQuests.Length)
            {
                QuestData nextQuest = incompleteQuests[currentQuestIndex];
                if (nextQuest.questDescription != null && nextQuest.questDescription.Count > 0)
                {
                    Debug.Log("QM: Playing next quest description for quest: " + nextQuest.questName);
                    DialogManager.Instance.PlaySequencesAndQuestDescription(nextQuest.questDescription[0]);
                }
            }
        }
    }

    public void ClearAllShapes()
    {
        var playerShapes = FindObjectsOfType<ShapeComponent>().ToList();
        foreach (var shape in playerShapes)
        {
            Destroy(shape.gameObject);
        }

        GameObject[] spawnableObjects = GameObject.FindGameObjectsWithTag("SpawnedObject");
        foreach (GameObject spawnableObject in spawnableObjects)
        {
            Destroy(spawnableObject);
        }
    }

    public void CheckShapes()
    {
        if (currentQuestIndex < incompleteQuests.Length)
        {
            QuestData currentQuest = incompleteQuests[currentQuestIndex];
            bool questCompleted = CheckQuestCompletionByName(currentQuest.questName);

            if (questCompleted)
            {
                CompleteCurrentQuest();
            }
        }
    }
}
