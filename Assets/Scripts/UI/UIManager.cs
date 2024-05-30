using Thrakal;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    // Spillerens panel i bunden.
    [SerializeField] private CanvasGroup GamePanel;
    // Slet knappen game object.
    [SerializeField] private GameObject deleteZone;

    // Dialog teksten.
    [SerializeField] private TextMeshProUGUI DialogText;
    // Dialog boksen.
    [SerializeField] private CanvasGroup DialogBox;
    // Titlen på Scenen.
    [SerializeField] private TextMeshProUGUI GameTitle;
    [SerializeField] private CanvasGroup TitleGroup;

    // baggrunds prefabbem
    [SerializeField] private GameObject Background;
    // Quest progress
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI RequiredAmount;
    [SerializeField] private CanvasGroup ScoreGroup;

    // Reference sprite for the winning condition
    [SerializeField] private Image referenceImage;

    public void showGamePanel()
    {
        GamePanel.alpha = 1;
        GamePanel.interactable = true;
        GamePanel.blocksRaycasts = true;
    }

    public void hideGamePanel()
    {
        GamePanel.alpha = 0;
        GamePanel.interactable = false;
        GamePanel.blocksRaycasts = false;
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
        DialogBox.interactable = true;
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
        DialogBox.interactable = false;
        DialogBox.blocksRaycasts = false;
    }

    public void ShowDeleteZone()
    {
        if (deleteZone != null)
            deleteZone.SetActive(true);
    }

    public void HideDeleteZone()
    {
        if (deleteZone != null)
            deleteZone.SetActive(false);
    }

    public void ShowReferenceSprite(Sprite reference)
    {
        if (referenceImage != null)
        {
            referenceImage.sprite = reference;
            referenceImage.gameObject.SetActive(true);
            referenceImage.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void HideReferenceSprite()
    {
        if (referenceImage != null)
        {
            Color color = referenceImage.color;
            color.a = 0.0f;
            referenceImage.color = color;
            referenceImage.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int current, int required)
    {
        if (currentScore != null && RequiredAmount != null)
        {
            currentScore.text = current.ToString();
            RequiredAmount.text = required.ToString();
            ScoreGroup.alpha = 1;
            ScoreGroup.interactable = true;
            ScoreGroup.blocksRaycasts = true;
        }
    }

    public void HideScore()
    {
        if (ScoreGroup != null)
        {
            ScoreGroup.alpha = 0;
            ScoreGroup.interactable = false;
            ScoreGroup.blocksRaycasts = false;
        }
    }

    // Ny metode til at skjule alle UI-elementer
    public void HideAllUIElements()
    {
        HideReferenceSprite();
        HideDeleteZone();
        hideDialog();
        disableTitle();
        hideGamePanel();
        hideButtons();
        HideScore();
    }
}
