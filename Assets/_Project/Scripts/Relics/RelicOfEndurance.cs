using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfEndurance", menuName = "Create / Relic / RelicOfEndurance")]
public class RelicOfEndurance : RelicTemplate
{
    public int attackBuff = 5;

    public override void DuringBattle(GameObject player)
    {
        Debug.Log("temporarily increasing attack");
    }
}
