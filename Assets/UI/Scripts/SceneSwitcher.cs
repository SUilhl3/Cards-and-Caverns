using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private const string LastSceneKey = "LastSceneIndex";
    private const int OptionsSceneIndex = 3;
    private const string PauseMenuOpenKey = "PauseMenuOpen";
    private const string ConfirmationOpenKey = "ConfirmationOpen";

    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != OptionsSceneIndex)
        {
            PlayerPrefs.SetInt(LastSceneKey, currentSceneIndex);
            PlayerPrefs.Save();
        }
    }

    public void LoadOptions()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(OptionsSceneIndex);
    }

    public void CloseOptionsAndReturn()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            int lastSceneIndex = PlayerPrefs.GetInt(LastSceneKey);

            PlayerPrefs.SetInt(PauseMenuOpenKey, 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene(lastSceneIndex);
        }
        else
        {
            Debug.LogWarning("No last scene found to return to!");
        }
    }

    public void LoadTutorial()
    {
        ClearPauseState();
        SceneManager.LoadScene(2);
    }

    public void LoadMap()
    {
        ClearPauseState();
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        ClearPauseState();
        SceneManager.LoadScene(0);
    }

    public void LoadLastScene()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            int lastSceneIndex = PlayerPrefs.GetInt(LastSceneKey);
            SceneManager.LoadScene(lastSceneIndex);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ClearPauseState()
    {
        PlayerPrefs.SetInt(PauseMenuOpenKey, 0);
        PlayerPrefs.SetInt(ConfirmationOpenKey, 0);
        PlayerPrefs.Save();
    }
}
