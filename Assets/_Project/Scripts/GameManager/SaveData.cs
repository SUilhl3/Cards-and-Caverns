using UnityEngine;
using System.Collections;


[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public string sceneName;
    public int[] completedBattles;
    public int battlesWon;
    public int enemiesKilled;
}
