using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleWealth", menuName = "Create / Relic / RelicOfLittleWealth")]
public class RelicOfLittleWealth : RelicTemplate
{
    public int coins = 200;

    public override void onAcquire(GameObject player)
    {
        Debug.Log("added 200 coins");
    }
}
