using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    public void SaveGame()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform is not assigned!");
            return;
        }

        SaveData saveData = new SaveData
        {
            playerPosition = player.position,
            sceneName = SceneManager.GetActiveScene().name
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved! Scene: " + saveData.sceneName);
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Debug.LogWarning("No save data found!");
            return;
        }

        string json = PlayerPrefs.GetString("SaveData");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        SceneManager.LoadScene(saveData.sceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
