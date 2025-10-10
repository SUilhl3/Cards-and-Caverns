using UnityEngine;

[CreateAssetMenu(fileName = "NewRelic", menuName = "Create / Relic")]
public class RelicTemplate : ScriptableObject
{
    public string relicName;
    public string relicDescription;
    public Sprite relicImg;

    //takes effect as soon as you acquire it
    //something like a permanent increase in health 
    public virtual void onAcquire (GameObject player) { }

    //takes effect right at the beginning of battle
    //something temporary like an increase in your first attack or giving an extra card play or something
    public virtual void OnBattleStart(GameObject player) { }

    //takes effect at the end of battle
    //something like increasing your health after a battle by a % or number
    public virtual void OnBattleFinish(GameObject player) { }

    //takes effect during battle 
    //something like increasing attack if your health goes below a %
    public virtual void DuringBattle(GameObject player) { }

}
