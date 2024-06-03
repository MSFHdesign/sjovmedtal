using Thrakal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogManager : Singleton<DialogManager>
{
    public Sequence[] sequences;
    public Queue<string> dialogQueue;
    public string questPath = "QuestData/Shapes"; // Standard sti til quests

    private bool isDialogActive = false; // Boolean to track if dialog is active
    private Coroutine currentDialogCoroutine; // To track the current running coroutine

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

    public void PlaySequencesAndQuestDescription(string questDescription)
    {
        StopDialog(); // Stop any ongoing dialog before starting a new one
        currentDialogCoroutine = StartCoroutine(PlaySequencesAndQuestDescriptionCoroutine(questDescription));
    }

    private IEnumerator PlaySequencesAndQuestDescriptionCoroutine(string questDescription)
    {
        Debug.Log("DialogManager: Starting to play sequences.");
        isDialogActive = true;

        // Assuming each sequence has a dialog array to play
        foreach (var sequence in sequences)
        {
            foreach (var dialogue in sequence.dialog)
            {
                if (!isDialogActive) yield break; // Exit coroutine if dialog is stopped
                yield return StartCoroutine(PlayDialog(dialogue));
            }
        }

        Debug.Log("DialogManager: Finished playing sequences. Now playing quest description.");

        // Play the quest description
        if (!string.IsNullOrEmpty(questDescription))
        {
            yield return StartCoroutine(PlayDialog(questDescription));
        }

        Debug.Log("DialogManager: Finished playing quest description.");
        isDialogActive = false;
    }

    private IEnumerator PlayDialog(string dialog)
    {
        Debug.Log("Playing dialog: " + dialog);
        UIManager.Instance.showDialog(dialog); // Example to show the dialog using UIManager
        float words = dialog.Split(' ').Length;
        float dynamicWaitTime = Mathf.Max(words / 3, 2); // Adjust according to your wordsPerSecond and minWaitTime
        yield return new WaitForSeconds(dynamicWaitTime);
        UIManager.Instance.hideDialog();
    }

    public void StopDialog()
    {
        if (currentDialogCoroutine != null)
        {
            StopCoroutine(currentDialogCoroutine);
            currentDialogCoroutine = null;
        }

        isDialogActive = false;
        dialogQueue.Clear(); // Optionally clear the queue
        UIManager.Instance.hideDialog(); // Hide any visible dialog
        Debug.Log("DialogManager: Dialog stopped.");
    }
}
