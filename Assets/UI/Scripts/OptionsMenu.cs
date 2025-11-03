using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject gameplayPanel;
    public GameObject creditsPanel;
    public GameObject resetPanel;
    public GameObject inputPanel;

    void Start()
    {
        soundPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);

    }

    public void ShowSoundPanel()
    {
        soundPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowInputPanel()
    {
        soundPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(true);
    }

    public void ShowGamePlayPanel()
    {
        soundPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowCreditsPanel()
    {
        soundPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(true);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowResetPanel()
    {
        soundPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(true);
        inputPanel.SetActive(false);
    }
}
