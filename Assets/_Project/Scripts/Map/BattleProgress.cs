using UnityEngine;

public class BattleProgress : MonoBehaviour
{
    public static BattleProgress instance;

    public int maxBattles = 20;   
    public int currentBattleID = -1; 

    public int battlesWon = 0; 
    public int enemiesKilled = 0;

    public delegate void ProgressUpdated();
    public static event ProgressUpdated OnProgressUpdated;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Load();  
    }

    public void CompleteBattle(int id)
    {
        if (id < 0 || id >= maxBattles) return;

        battlesWon++; 
        currentBattleID = battlesWon; 

        Save(); 

        OnProgressUpdated?.Invoke(); 
    }

    public void IncrementEnemiesKilled()
    {
        enemiesKilled++;
        OnProgressUpdated?.Invoke();
    }

    public void Save()
    {
        SaveData data = new SaveData
        {
            battlesWon = battlesWon,
            enemiesKilled = enemiesKilled
        };

        PlayerPrefs.SetString("BattleProgress", JsonUtility.ToJson(data)); 
        PlayerPrefs.Save(); 
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("BattleProgress"))
            return;

        string json = PlayerPrefs.GetString("BattleProgress");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        battlesWon = data.battlesWon;
        enemiesKilled = data.enemiesKilled;
        currentBattleID = battlesWon; 

        OnProgressUpdated?.Invoke();
    }

    public void IncrementProgressForRestScene()
    {
        for (int i = 0; i < maxBattles; i++)
        {
            if (i <= battlesWon) 
            {
                CompleteBattle(i);
                Debug.Log("Battle " + i + " marked as completed via rest scene.");
                break;  
            }
        }
    }
}
