using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelicManager : MonoBehaviour
{
    public List<RelicTemplate> allAvailableRelics = new List<RelicTemplate>(); //list of all available relics
    public List<RelicTemplate> relicList = new List<RelicTemplate>(); // list of relics player acquired
    public PlayerCombatant player;
    [SerializeField] CombatManager combatManager;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] CoinCount coinCount;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        combatManager = FindAnyObjectByType<CombatManager>(); ;
        enemyManager = FindAnyObjectByType<EnemyManager>();
        player = FindAnyObjectByType<PlayerCombatant>();
        coinCount = FindAnyObjectByType<CoinCount>();
    }

    //just for testing for now
    private void Update()
    {
        //adding random relic
        if(Input.GetKeyDown(KeyCode.V))
        {
            SelectRandomRelic();
        }
      
        //testing relic method calls
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnBattleStartCalls();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            OnBattleFinishCalls();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            DuringBattleCalls();
        }
    }
    //selects a random relic from the list and adds it to the player 
    //might need to add weights or lower % chance for higher value relics later
    public void SelectRandomRelic ()
    {
        int chosenRelic = Random.Range(0, allAvailableRelics.Count);
        RelicTemplate relicToAdd = allAvailableRelics[chosenRelic];
        AddRelic(relicToAdd);
        allAvailableRelics.Remove(relicToAdd); //remove the added relic to avoid duplicates
    }

    //adds a relic to the list and calls the onAcquire method
    public void AddRelic(RelicTemplate relic)
    {
        relicList.Add(relic);
        relic.onAcquire(player, coinCount);
    }

    //call all the onBattleStart methods for each relic 
    public void OnBattleStartCalls()
    {
        foreach (var relic in relicList)
        {
            relic.OnBattleStart(player, combatManager, enemyManager);
        }
    }

    //calls all the OnBattleFinish methods for each relic 
    public void OnBattleFinishCalls()
    {
        foreach(var relic in relicList)
        {
            relic.OnBattleFinish(player, coinCount);
        }
    }

    //calls all the DuringBattle methods for each relic
    public void DuringBattleCalls()
    {
        foreach (var relic in relicList)
        {
            relic.DuringBattle(player);
        }
    }

}
