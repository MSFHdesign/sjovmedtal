using UnityEngine;
using UnityEngine.UI;

public class ToggleDebug : MonoBehaviour
{
    Toggle toggle;

    void Start()
    {
        // Hent Toggle-komponenten fra GameObjectet
        toggle = GetComponent<Toggle>();

        // Lyt efter ændringer i toggles tilstand og kald ToggleStateChanged metoden
        toggle.onValueChanged.AddListener(ToggleStateChanged);
    }

    // Metode kaldt når toggles tilstand ændres
    void ToggleStateChanged(bool newState)
    {
        // Hvis togglen er blevet aktiveret
        if (newState)
        {
            Debug.Log("Toggle er blevet aktiveret!");
        }
        // Hvis togglen er blevet deaktiveret
        else
        {
            Debug.Log("Toggle er blevet deaktiveret!");
        }
    }

    void OnDestroy()
    {
        // Fjern lytteren for at undgå lækage
        toggle.onValueChanged.RemoveListener(ToggleStateChanged);
    }
}
