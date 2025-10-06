using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public List<RelicTemplate> relicList = new List<RelicTemplate>();
    public GameObject player;

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
