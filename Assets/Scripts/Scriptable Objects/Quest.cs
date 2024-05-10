using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quests", order = 2)]
public class Quest : ScriptableObject
{
    public string QuestName;
    public ShapeEntry[] Shapes;
    public string[] Description;
    public bool IsCompleted;
    public int RequiredAmount;
    public int CurrentAmount = 0;
    public int RewardId;

    public void CheckCompletion()
    {
        if (IsCompleted)
        {
            CompleteQuest();
        }

        else if (CurrentAmount >= RequiredAmount)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        IsCompleted = true;
        // Tilføj kode til at håndtere quest completion, f.eks. opdater UI eller belønninger
        Debug.Log($"Quest completed: {QuestName}");
    }

    public void UpdateProgress(int amount)
    {
        CurrentAmount += amount;
        CheckCompletion();
    }
}

[System.Serializable]
public class ShapeEntry
{
    public Shapes Shape;
    public Sprite Sprite;
}

public enum Shapes
{
    Circle,
    Square,
    Triangle,
    Star
}
