using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public enum ShapeType
    {
        Circle,
        Square,
        Triangle,
        Oval,
        Crescent,
        Cylinder
    }

    [System.Serializable]
    public class ShapeRequirement
    {
        public ShapeType shapeType;
        public Vector2 position; // Position in a 2D plane
        public string hint;
    }

    public string questName;
    public Sprite winningReference; // Reference sprite for the winning condition
    public List <string> questDescription;
    public List<ShapeRequirement> requiredShapes; // List of shapes required to complete the quest
    public int requiredAmount;
    public int currentAmount;
    public string status;
    public bool isCompleted;
    public int rewardId;
}
