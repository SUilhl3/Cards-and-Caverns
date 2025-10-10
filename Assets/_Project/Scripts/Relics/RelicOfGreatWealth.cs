using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatWealth", menuName = "Create / Relic / RelicOfGreatWealth")]

public class RelicOfGreatWealth : RelicTemplate
{
    public int coins = 400;

    public override void onAcquire(GameObject player)
    {
        Debug.Log("Adding 400 coins");
    }
}
