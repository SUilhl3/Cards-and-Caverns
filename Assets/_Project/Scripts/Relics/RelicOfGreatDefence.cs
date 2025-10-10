using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatDefence", menuName = "Create / Relic / RelicOfGreatDefence")]

public class RelicOfGreatDefence : RelicTemplate
{
    public int defenceBuff = 15;

    public override void OnBattleStart(GameObject player)
    {
        Debug.Log("Increasing defence");
    }
}
