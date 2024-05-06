using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Sequence[] sequences;

    void Start()
    {
        // Ensure that sequences is not empty and contains at least one Sequence with a dialogue.
        if (sequences != null && sequences.Length > 0 && sequences[0].dialog.Length > 0)
        {
            string dialog = sequences[0].GetNextDialogue(); // Declare 'dialog' as a string
            if (dialog != null)
            {
                Debug.Log(dialog);
            }
            else
            {
                Debug.Log("No more dialogues available.");
            }
        }
        else
        {
            Debug.Log("Sequences are not properly initialized.");
        }
    }
}
