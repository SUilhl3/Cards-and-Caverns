using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatHealing", menuName = "Create / Relic / RelicOfGreatHealing")]
public class RelicOfGreatHealing : RelicTemplate
{
    public int healAmt = 15;

    public override void OnBattleFinish(GameObject player)
    {
        //can call a healing method on the player here
        Debug.Log("healing the player");
    }
}
