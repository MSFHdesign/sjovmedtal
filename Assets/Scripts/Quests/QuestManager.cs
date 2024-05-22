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
        return quests.Where(quest => !quest.isCompleted).ToArray();
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

        // Find all ShapeComponent instances in the scene (representing the player's created shapes)
        var playerShapes = FindObjectsOfType<ShapeComponent>().ToList();
        Debug.Log($"QM: Player shapes count: {playerShapes.Count}, Required shapes count: {quest.requiredShapes.Count}");

        foreach (var shape in playerShapes)
        {
            Debug.Log($"QM: Player shape found: {shape.shapeType}");
        }

        // If the number of player shapes is less than the required shapes, the quest is not completed
        if (playerShapes.Count < quest.requiredShapes.Count)
        {
            Debug.Log("QM: Not enough shapes created by player.");
            quest.isCompleted = false;
            return false;
        }

        // Check if each required shape is present among the player's created shapes
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

        // If all required shapes are present, the quest is completed
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
            UIManager.Instance.ShowReferenceSprite(currentQuest.winningReference); // Vis reference-sprite

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
            StartNextQuest();
        }
    }

    // Tilføj denne metode for at tjekke former
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
