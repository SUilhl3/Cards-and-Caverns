using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public GameObject ConfirmationPanel;

    private const string PauseMenuOpenKey = "PauseMenuOpen";
    private const string ConfirmationOpenKey = "ConfirmationOpen";

    void Start()
    {
        bool wasPauseMenuOpen = PlayerPrefs.GetInt(PauseMenuOpenKey, 0) == 1;
        bool wasConfirmationOpen = PlayerPrefs.GetInt(ConfirmationOpenKey, 0) == 1;

        PauseMenuPanel.SetActive(wasPauseMenuOpen);
        ConfirmationPanel.SetActive(wasConfirmationOpen);

        if (wasPauseMenuOpen)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

        PauseMenuPanel.SetActive(false);
        ConfirmationPanel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        PlayerPrefs.SetInt(PauseMenuOpenKey, 1);
        PlayerPrefs.Save();
    }

    public void HidePauseMenu()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt(PauseMenuOpenKey, 0);
        PlayerPrefs.Save();
    }

    public void ShowConfirmationPanel()
    {
        ConfirmationPanel.SetActive(true);
        PlayerPrefs.SetInt(ConfirmationOpenKey, 1);
        PlayerPrefs.Save();
    }

    public void HideConfirmationPanel()
    {
        ConfirmationPanel.SetActive(false);
        PlayerPrefs.SetInt(ConfirmationOpenKey, 0);
        PlayerPrefs.Save();
    }
}
