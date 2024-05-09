using Thrakal;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
   // Spillerens panel i bunden.
    [SerializeField] private CanvasGroup GamePanel;
    [SerializeField] private CanvasGroup DeletePanel;
   // Dialog teksten. 
    [SerializeField] private TextMeshProUGUI DialogText;
    // Dialog boksen.
    [SerializeField] private CanvasGroup DialogBox;
    // Titlen på Scenen.
    [SerializeField] private TextMeshProUGUI GameTitle;
    [SerializeField] private CanvasGroup TitleGroup;

    // baggrunds prefabbem
    [SerializeField] private GameObject Background;





    public void showGamePanel()
    {
        GamePanel.alpha = 1;
        GamePanel.interactable= true;
        GamePanel.blocksRaycasts = true;
        DeletePanel.alpha = 1;
        DeletePanel.interactable = true;
        DeletePanel.blocksRaycasts = true;
    }
    public void hideGamePanel()
    {
        GamePanel.alpha = 0;
        GamePanel.interactable = false;
        GamePanel.blocksRaycasts = false;
        DeletePanel.alpha = 0;
        DeletePanel.interactable = false;
        DeletePanel.blocksRaycasts = false;
    }


    public void showTitle(string message)
    {
        GameTitle.text = message;
        TitleGroup.alpha = 1;
        TitleGroup.interactable = true;
        TitleGroup.blocksRaycasts = true;
    }

    public void disableTitle()
    {
        TitleGroup.alpha = 0;
        TitleGroup.interactable = false;
        TitleGroup.blocksRaycasts = false;
    }

 

    public void showDialog(string message)
    {

    DialogText.text = message;
        DialogBox.alpha = 1;
        DialogBox.interactable= true;
        DialogBox.blocksRaycasts = true;

    }
    public void hideButtons()
    {
        GamePanel.alpha = 0;
        GamePanel.interactable = false;
        GamePanel.blocksRaycasts = false;
    }
    public void hideDialog()
    {
        DialogText.text = "";
        DialogBox.alpha = 0;
        DialogBox.interactable= false;
        DialogBox.blocksRaycasts = false;

    }
}

