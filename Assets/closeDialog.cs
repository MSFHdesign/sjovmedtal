using UnityEngine;

public class DialogButtonController : MonoBehaviour
{
    public void OnStopDialogButtonClick()
    {
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.StopDialog();
         
        }
    }
}
