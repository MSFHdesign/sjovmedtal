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
            }
        }
    }

    public void PlaySequencesAndQuestDialogs()
    {
        StartCoroutine(PlaySequencesAndQueue());
    }

    private IEnumerator PlaySequencesAndQueue()
    {
        // Assuming each sequence has a dialog array to play
        foreach (var sequence in sequences)
        {
            foreach (var dialogue in sequence.dialog)
            {
                yield return StartCoroutine(PlayDialog(dialogue));
            }
        }

        // Now process the dialog queue
        while (dialogQueue.Count > 0)
        {
            var dialog = dialogQueue.Dequeue();
            yield return StartCoroutine(PlayDialog(dialog));
        }
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
}
