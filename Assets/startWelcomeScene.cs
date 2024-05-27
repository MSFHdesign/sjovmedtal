using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartWelcomeScene : MonoBehaviour
{
    private Sequence sequence;
    private DialogManager dialogManager;

    public string titleName;
    public float wordsPerSecond = 3;
    public float minWaitTime = 2;

    void Start()
    {
        // Vis spilpanelet
        UIManager.Instance.ShowGamePanel();

        string sceneName = SceneManager.GetActiveScene().name;
        UIManager.Instance.ShowDeleteZone();

        // Viser titlen baseret på scenens navn eller en specificeret titel
        if (string.IsNullOrEmpty(titleName))
        {
            UIManager.Instance.ShowTitle(sceneName);
        }
        else
        {
            UIManager.Instance.ShowTitle(titleName);
        }

        dialogManager = DialogManager.Instance;
        sequence = dialogManager.GetSequence(sceneName);

        if (sequence != null)
        {
            StartCoroutine(DisplayDialogSequence());
        }
        else
        {
            Debug.LogError("SG: Sequence not found for the scene: " + sceneName);
        }
    }

    IEnumerator DisplayDialogSequence()
    {
        foreach (var dialogue in sequence.dialog)
        {
            UIManager.Instance.ShowDialog(dialogue);

            // Beregn ventetid baseret på antal ord i dialogen
            float words = dialogue.Split(' ').Length;
            float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime);
            yield return new WaitForSeconds(dynamicWaitTime);
        }

        yield return new WaitForSeconds(1f);

        UIManager.Instance.HideDialog();
    }
}
