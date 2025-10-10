using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGoodHealth", menuName = "Create / Relic / RelicOfGoodHealth")]

public class RelicOfGoodHealth : RelicTemplate
{
    public int healthIncrease = 10;

    public override void onAcquire(GameObject player)
    {
        Debug.Log("permanently increasing health");
    }
}
