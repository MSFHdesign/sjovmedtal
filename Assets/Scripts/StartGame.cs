using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private Sequence sequence;

    public string titleName;
    public float wordsPerSecond = 3;
    public float minWaitTime = 2;
    public string questPath = "QuestData/Shapes"; // Standard sti til quests

    void Start()
    {
        UIManager.Instance.showGamePanel();

        Debug.Log("SG: Loading quests from path: " + questPath);
        // Indlæs quests fra den angivne sti
        QuestManager.Instance.LoadQuestsFromPath(questPath);

        // Start den første quest
        QuestManager.Instance.StartNextQuest();
  
        string sceneName = SceneManager.GetActiveScene().name;
        UIManager.Instance.ShowDeleteZone();
        if (string.IsNullOrEmpty(titleName))
        {
            UIManager.Instance.showTitle(sceneName);
        }
        else
        {
            UIManager.Instance.showTitle(titleName);
        }

        sequence = DialogManager.Instance.GetSequence(sceneName);
        if (sequence != null)
        {
            StartCoroutine(DisplayDialogs());
        }
        else
        {
            Debug.LogError("SG: Sequence not found for the scene: " + sceneName);
        }
    }

    IEnumerator DisplayDialogs()
    {
        foreach (var dialogue in sequence.dialog)
        {
            UIManager.Instance.showDialog(dialogue);

            float words = dialogue.Split(' ').Length;
            float dynamicWaitTime = Mathf.Max(words / wordsPerSecond, minWaitTime);
            yield return new WaitForSeconds(dynamicWaitTime);
        }

        yield return new WaitForSeconds(1f);

        UIManager.Instance.hideDialog();
    }
}
