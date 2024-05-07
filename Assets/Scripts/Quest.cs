using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Sequence sandboxSequence;
    public UIs sandboxUI;

    void Start()
    {
        // Assuming DialogManager provides a sequence for dialogs related to quests
        sandboxSequence = DialogManager.Instance.GetSequence(0);

        // Assuming UIManager manages different UI panels or elements and you want to get a UI element named "Sandbox"
        sandboxUI = UIManager.Instance.GetUI("Sandbox");
    }
}
