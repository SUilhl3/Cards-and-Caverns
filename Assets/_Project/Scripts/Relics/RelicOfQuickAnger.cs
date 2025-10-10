using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfQuickAnger", menuName = "Create / Relic / RelicOfQuickAnger")]
public class RelicOfQuickAnger : RelicTemplate
{
    public int attackAmount = 5;

    public override void OnBattleStart(GameObject player)
    {
        Debug.Log("Increasing first attack");
    }
}
