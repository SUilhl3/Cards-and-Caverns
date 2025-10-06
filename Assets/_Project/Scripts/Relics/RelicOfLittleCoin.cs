using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleCoin", menuName = "Create / Relic / RelicOfLittleCoin")]
public class RelicOfLittleCoin : RelicTemplate
{
    public float coinMultiplier = 1.25f;

    public override void OnBattleFinish(GameObject player)
    {
        //apply multiplier to coins to be won after battle 
        Debug.Log("applied multiplier");
    }
}
