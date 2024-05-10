using Thrakal;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public Quest[] quests;

    public override void Awake()
    {
        base.Awake();
        quests = Resources.LoadAll<Quest>("Quests");
    }

    public Quest GetQuest(string questName)
    {
        foreach (Quest quest in quests)
        {
            if (quest.QuestName == questName)
                return quest;
        }
        Debug.Log("QM: Quest not found: " + questName);
        return null;
    }

    public void UpdateStatus(string questName, string status)
    {
        Quest quest = GetQuest(questName);
        if (quest != null)
        {
            quest.Status = status;
            Debug.Log($"QM: Quest '{questName}' status updated to: {status}");
        }
        else
        {
            Debug.LogError($"QM: Quest with name '{questName}' not found.");
        }
    }


    public bool CheckQuestCompletionByName(string questName)
    {
        Quest quest = GetQuest(questName);

        if (quest == null)
        {
            Debug.LogError($"QM: Quest with name '{questName}' not found.");
            return false;
        }


        if (quest.CurrentAmount < quest.RequiredAmount)
        {
            quest.IsCompleted = false;
            Debug.Log($"QM: Quest '{quest.QuestName}' not completed: {quest.CurrentAmount} / {quest.RequiredAmount}");
            CompleteQuest(quest);
            return false;
        }

        if (quest.IsCompleted)
        {
            Debug.Log($"QM: Quest '{quest.QuestName}' already completed.");
            return true;
        }

        return false;
    }

    private void UpdateCurrentAmount(string questName, int amount)
    {
        Quest quest = GetQuest(questName);
        if (quest == null)
        {
            Debug.LogError($"Quest with name '{questName}' not found.");
            return;
        }

        quest.CurrentAmount += amount;
        Debug.Log($"Quest '{quest.QuestName}' updated: Current amount is now {quest.CurrentAmount}");

        if (quest.CurrentAmount >= quest.RequiredAmount)
        {
            CompleteQuest(quest);
        }
    }


    private void CompleteQuest(Quest quest)
    {
        if (quest.IsCompleted)
        {
            GiveReward(quest.RewardId);
            Debug.Log($"Quest completed: {quest.QuestName}");
        }
    }

    private void GiveReward(int rewardId)
    {
        // Implementer logikken for at give belønninger her
        Debug.Log($"Reward given: {rewardId}");
    }
}
