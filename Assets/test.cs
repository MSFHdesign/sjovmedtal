using System.Collections;
using UnityEngine;

public class test : MonoBehaviour
{
    private Sequence sequence; // Holder den sekvens, der hentes fra DialogManager

    void Start()
    {
        UIManager.Instance.showGamePanel();
        // Antag at navnet på den ønskede sekvens er "ShapeMiniGame", som set på billedet
        sequence = DialogManager.Instance.GetSequence("ShapeMiniGame");
        if (sequence != null) // Sikrer, at sekvensen er hentet korrekt
        {
            StartCoroutine(DisplayDialogs());
        }
        else
        {
            Debug.LogError("Sekvens ikke fundet: ShapeMiniGame");
        }
    }

    IEnumerator DisplayDialogs()
    {
        foreach (var dialogue in sequence.dialog)
        {
            UIManager.Instance.showDialog(dialogue);
            yield return new WaitForSeconds(20); // Vent 20 sekunder mellem hver dialog
        }
        UIManager.Instance.hideDialog(); // Skjuler dialogen når alle er vist
    }
}
