using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    //Player Stats
    public int maxHealth = 30;
    public int currentHealth = 30;
    public int drawPerTurn = 5;

    PlayerCombatant player;

    private void Awake()
    {
        //player = FindAnyObjectByType<PlayerCombatant>();
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindAnyObjectByType<PlayerCombatant>();
        UpdateStats();
    }
    public void UpdateStats()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerCombatant>();
        }

        //initialize the player stats if it is the first scene / battle
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            maxHealth = player.maxHealth;
            currentHealth = player.currentHealth;
            drawPerTurn = player.drawPerTurn;
        }


        player.maxHealth = maxHealth;
        player.currentHealth = currentHealth;
        player.drawPerTurn = drawPerTurn;

        player.UpdateHealthText();
    }

    //updates the stats to be used during the next battle 
    public void SaveStats()
    {
        maxHealth = player.maxHealth;
        currentHealth = player.currentHealth;
        drawPerTurn = player.drawPerTurn;
    }

    public void Rest()
    {
        currentHealth = maxHealth;
        SceneManager.LoadScene("levelSelect");
    }
}
