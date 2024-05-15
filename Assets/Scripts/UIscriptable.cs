using UnityEngine;

[CreateAssetMenu(fileName = "New UI", menuName = "ScriptableObjects/UI", order = 0)]

public class UIs : ScriptableObject
{
    public string uiName; // For at undgå konflikter med MonoBehaviour's 'name' property
    public GameObject uiPrefab; // Refererer til UI Prefab
    public bool isActiveByDefault; // Om UI skal være aktivt som standard




}


