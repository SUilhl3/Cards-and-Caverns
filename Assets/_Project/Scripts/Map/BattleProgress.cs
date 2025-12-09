using UnityEngine;

public class BattleProgress : MonoBehaviour
{
    public static BattleProgress instance;

    public int maxBattles = 20;    
    public bool[] completed;     
    public bool[] unlocked;      

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        completed = new bool[maxBattles];
        unlocked = new bool[maxBattles];

        Load();
    }

    public void CompleteBattle(int id)
    {
        if (id < 0 || id >= maxBattles) return;

        completed[id] = true;

        if (id + 1 < unlocked.Length)
            unlocked[id + 1] = true;

        Save();
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.completedBattles = GetCompletedList();
        PlayerPrefs.SetString("BattleProgress", JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    public void Load()
    {
        unlocked[0] = true;

        if (!PlayerPrefs.HasKey("BattleProgress"))
            return;

        string json = PlayerPrefs.GetString("BattleProgress");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data.completedBattles == null) return;

        foreach (int id in data.completedBattles)
        {
            if (id >= 0 && id < maxBattles)
            {
                completed[id] = true;
                if (id + 1 < unlocked.Length)
                    unlocked[id + 1] = true;
            }
        }
    }

    private int[] GetCompletedList()
    {
        int count = 0;

        for (int i = 0; i < completed.Length; i++)
            if (completed[i]) count++;

        int[] result = new int[count];
        int index = 0;

        for (int i = 0; i < completed.Length; i++)
            if (completed[i]) result[index++] = i;

        return result;
    }
}
