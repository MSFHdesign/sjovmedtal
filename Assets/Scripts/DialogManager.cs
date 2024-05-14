using Thrakal;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    public Sequence[] sequences;

    public override void Awake()
    {
        base.Awake();

        sequences = Resources.LoadAll<Sequence>("Sequences");
    }

    public Sequence GetSequence(int index)
    {
        return sequences[index];
    }

    public Sequence GetSequence(string sequenceName)
    {
        for(int i = 0; i < sequences.Length; i++)
        {
            if (sequences[i].sequenceName == sequenceName)
                return sequences[i];
        }

        return null;
    }
}
