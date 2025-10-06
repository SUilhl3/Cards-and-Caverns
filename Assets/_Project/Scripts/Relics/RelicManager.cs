using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public List<RelicTemplate> allAvailableRelics = new List<RelicTemplate>(); //list of all available relics
    public List<RelicTemplate> relicList = new List<RelicTemplate>(); // list of relics player acquired
    public GameObject player;

    //selects a random relic from the list and adds it to the player 
    //might need to add weights or lower % chance for higher value relics later
    public void SelectRandomRelic ()
    {
        int chosenRelic = Random.Range(0, allAvailableRelics.Count);
        RelicTemplate relicToAdd = allAvailableRelics[chosenRelic];
        AddRelic(relicToAdd);
    }

    //adds a relic to the list and calls the onAcquire method
    public void AddRelic(RelicTemplate relic)
    {
        relicList.Add(relic);
        relic.onAcquire(player);
    }

    //call all the onBattleStart methods for each relic 
    public void OnBattleStartCalls()
    {
        foreach (var relic in relicList)
        {
            relic.OnBattleStart(player);
        }
    }

    //calls all the OnBattleFinish methods for each relic 
    public void OnBattleFinishCalls()
    {
        foreach(var relic in relicList)
        {
            relic.OnBattleFinish(player);
        }
    }

}
