using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfImmenseHealth", menuName = "Create / Relic / RelicOfImmenseHealth")]
public class RelicOfImmenseHealth : RelicTemplate
{
    public int healthIncrease = 20;

    public override void onAcquire(GameObject player)
    {
        Debug.Log("increasing health");
    }
}
