using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject displayPanel;
    public GameObject creditsPanel;

    void Start()
    {
        soundPanel.SetActive(false);
        displayPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void ShowSoundPanel()
    {
        soundPanel.SetActive(true);
        displayPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void ShowDisplayPanel()
    {
        soundPanel.SetActive(false);
        displayPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void ShowCreditsPanel()
    {
        soundPanel.SetActive(false);
        displayPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
