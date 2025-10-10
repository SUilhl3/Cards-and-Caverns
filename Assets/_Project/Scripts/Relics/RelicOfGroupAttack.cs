using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGroupAttack", menuName = "Create / Relic / RelicOfGroupAttack")]
public class RelicOfGroupAttack : RelicTemplate
{
    public int attackAmount = 3;

    public override void OnBattleStart(GameObject player)
    {
        Debug.Log("Attacking all enemies");
    }
}
