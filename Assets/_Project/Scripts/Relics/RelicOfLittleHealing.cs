using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleHealing", menuName = "Create / Relic / RelicOfLittleHealing")]
public class RelicOfLittleHealing : RelicTemplate
{
    public int healAmt = 5;

    public override void OnBattleFinish(GameObject player)
    {
        //can call a healing method on the player here
        Debug.Log("healing the player");
    }
}
