using Thrakal;
using UnityEngine;
using TMPro;
public class UIManager : Singleton<UIManager>
{
   
   [SerializeField] private CanvasGroup GamePanel;
   [SerializeField] private TextMeshProUGUI DialogText;
   [SerializeField] private CanvasGroup DialogBox;



    public void showGamePanel()
    {
        GamePanel.alpha = 1;
        GamePanel.interactable= true;
        GamePanel.blocksRaycasts = true;
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

