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
        Debug.Log("Pressed Sound Button");
        soundPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowInputPanel()
    {
        Debug.Log("Pressed Input Button");

        soundPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(true);
    }

    public void ShowGamePlayPanel()
    {
        Debug.Log("Pressed GamePlay Button");

        soundPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowCreditsPanel()
    {
        Debug.Log("Pressed Credits Button");

        soundPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        creditsPanel.SetActive(true);
        resetPanel.SetActive(false);
        inputPanel.SetActive(false);
    }

    public void ShowResetPanel()
    {
        Debug.Log("Pressed Reset Button");

        soundPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        resetPanel.SetActive(true);
        inputPanel.SetActive(false);
    }
}
