using UnityEngine;

[CreateAssetMenu(fileName = "New Sequence", menuName = "ScriptableObjects/Sequence", order = 1)]
public class Sequence : ScriptableObject
{
    public string name;
    public int value;
    public Shapes[] acceptedShapes;
    public string[] dialog;
    private int currentDialogueIndex = 0;
    public int rewardId;

    // This method returns the next dialogue entry or null if there are no more entries.
    public string GetNextDialogue()
    {
        if (currentDialogueIndex >= dialog.Length) // Corrected from 'length' to 'Length'
        {
            return null; // Optionally, you might reset the index here or handle it externally.
        }

        string response = dialog[currentDialogueIndex];
        currentDialogueIndex++;
        return response;
    }

    // Optionally, add a method to reset the dialogue sequence.
    public void ResetDialogue()
    {
        currentDialogueIndex = 0;
    }
}

public enum Shapes
{
    Circle,
    Square,
    Triangle
}
