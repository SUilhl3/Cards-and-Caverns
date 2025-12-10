using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public Vector3 startingPosition = Vector3.zero;

    public void SaveGame()
    {
        if (player == null) return;

        SaveData saveData = new SaveData
        {
            playerPosition = player.position,
            sceneName = SceneManager.GetActiveScene().name,
            battlesWon = BattleProgress.instance.battlesWon,
            enemiesKilled = BattleProgress.instance.enemiesKilled
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            StartNewGame();
            return;
        }

        string json = PlayerPrefs.GetString("SaveData");
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        SceneManager.LoadScene(saveData.sceneName);
        player.position = saveData.playerPosition;

        BattleProgress.instance.battlesWon = saveData.battlesWon;
        BattleProgress.instance.enemiesKilled = saveData.enemiesKilled;
    }

    public void StartNewGame()
    {
        BattleProgress.instance.battlesWon = 0;
        BattleProgress.instance.enemiesKilled = 0;

        if (player != null)
        {
            player.position = startingPosition;
        }

        SceneManager.LoadScene("levelSelect");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
