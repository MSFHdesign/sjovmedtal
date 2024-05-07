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


    }
}
