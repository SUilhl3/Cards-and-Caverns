using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    public string CurrentSceneName;
    public string LastSceneName;

    //Player Stats
    public int maxHealth = 30;
    public int currentHealth = 30;
    public int drawPerTurn = 5;

    PlayerCombatant player;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);

        CurrentSceneName = SceneManager.GetActiveScene().name;
        LastSceneName = CurrentSceneName;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateSceneValues(scene);
        player = FindAnyObjectByType<PlayerCombatant>();
        UpdateStats();
        
    }

    public void UpdateSceneValues(Scene scene)
    {
        if(CurrentSceneName == "EndGame" || CurrentSceneName == "EndGameWin" || CurrentSceneName == "OptionsMenu")
         {
            
         }
        else{LastSceneName = CurrentSceneName;}
        CurrentSceneName = scene.name;
        Debug.Log("Scene loaded: " + CurrentSceneName);
        Debug.Log("Last scene: " + LastSceneName);
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
