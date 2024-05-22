using Thrakal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : Singleton<DialogManager>
{
    public Sequence[] sequences;
    public Queue<string> dialogQueue;
    public string questPath = "QuestData/Shapes"; // Standard sti til quests

    public override void Awake()
    {
        base.Awake();

        sequences = Resources.LoadAll<Sequence>("Sequences");
        dialogQueue = new Queue<string>();

        // Load quests from the specified path
        LoadQuestsAndEnqueueDialogs(questPath);
    }

    public Sequence GetSequence(int index)
    {
        return sequences[index];
    }

    public Sequence GetSequence(string sequenceName)
    {
        for (int i = 0; i < sequences.Length; i++)
        {
            if (sequences[i].sequenceName == sequenceName)
                return sequences[i];
        }

        return null;
    }

    public void LoadQuestsAndEnqueueDialogs(string path)
    {
        QuestData[] quests = Resources.LoadAll<QuestData>(path);
        foreach (var quest in quests)
        {
            foreach (var dialog in quest.questDescription)
            {
                dialogQueue.Enqueue(dialog);
                Debug.Log("DialogManager: Enqueued dialog from quest: " + dialog);
            }
        }
    }

    public void PlaySequences()
    {
        StartCoroutine(PlaySequencesCoroutine());
    }

    private IEnumerator PlaySequencesCoroutine()
    {
        Debug.Log("DialogManager: Starting to play sequences.");

        // Assuming each sequence has a dialog array to play
        foreach (var sequence in sequences)
        {
            foreach (var dialogue in sequence.dialog)
            {
                yield return StartCoroutine(PlayDialog(dialogue));
            }
        }

        Debug.Log("DialogManager: Finished playing sequences.");
    }

    private IEnumerator PlayDialog(string dialog)
    {
        // Implement your dialog playing logic here
        Debug.Log("Playing dialog: " + dialog);
        UIManager.Instance.showDialog(dialog); // Example to show the dialog using UIManager
        float words = dialog.Split(' ').Length;
        float dynamicWaitTime = Mathf.Max(words / 3, 2); // Adjust according to your wordsPerSecond and minWaitTime
        yield return new WaitForSeconds(dynamicWaitTime);
        UIManager.Instance.hideDialog();
    }

    // Method to play current quest's description
    public void PlayQuestDescription(string description)
    {
        Debug.Log("DialogManager: Playing quest description: " + description);
        StartCoroutine(PlayQuestDescriptionCoroutine(description));
    }

    private IEnumerator PlayQuestDescriptionCoroutine(string description)
    {
        yield return StartCoroutine(PlayDialog(description));
    }
}
