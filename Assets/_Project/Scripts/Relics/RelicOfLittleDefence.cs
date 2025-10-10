using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleDefence", menuName = "Create / Relic / RelicOfLittleDefence")]

public class RelicOfLittleDefence : RelicTemplate
{
    public int defenceBuff = 5;

    public override void OnBattleStart(GameObject player)
    {
        Debug.Log("increasing defence");
    }
}
