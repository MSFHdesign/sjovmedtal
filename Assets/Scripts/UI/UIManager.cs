using Thrakal;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIs[] UI;
    public override void Awake()
    {
        base.Awake();
        UI = Resources.LoadAll<UIs>("UI");
        if (UI == null || UI.Length == 0)
        {
            Debug.LogError("No UI elements found in the Resources/UI folder.");
        }
        else
        {
            Debug.Log("Loaded " + UI.Length + " UI elements.");
        }
    }

    public UIs GetUI(int index)
    {
        if (index >= 0 && index < UI.Length)  
        {
            return UI[index];
        }
        return null;
    }

    public UIs GetUI(string UiName)
    {
        for (int i = 0; i < UI.Length; i++)
        {
            if (UI[i].uiName == UiName) 
                return UI[i];
        }

        return null;
    }
}
