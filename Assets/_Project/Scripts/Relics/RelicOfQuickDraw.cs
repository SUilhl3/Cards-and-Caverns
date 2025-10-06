using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfQuickDraw", menuName = "Create / Relic / RelicOfQuickDraw")]
public class RelicOfQuickDraw : RelicTemplate
{
    public int numExtraCards = 1;

    public override void OnBattleStart(GameObject player)
    {
        //draw an extra card 
        Debug.Log("Drawing extra card");
    }
}
