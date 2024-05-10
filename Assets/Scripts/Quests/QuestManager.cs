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
        Debug.Log("Quest not found: " + questName);
        return null;
    }

    public bool CheckQuestCompletionByName(string questName)
    {
        Quest quest = GetQuest(questName);

        if (quest == null)
        {
            Debug.LogError($"Quest with name '{questName}' not found.");
            return false;
        }

        if (quest.IsCompleted)
        {
            Debug.Log("Quest already completed.");
            return true;
        }

        if (quest.CurrentAmount >= quest.RequiredAmount)
        {
            quest.IsCompleted = true;
            Debug.Log($"Quest '{quest.QuestName}' completed: {quest.CurrentAmount} / {quest.RequiredAmount}");
            CompleteQuest(quest);
            return true;
        }

        Debug.Log($"Quest '{quest.QuestName}' not yet completed: {quest.CurrentAmount} / {quest.RequiredAmount}");
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
