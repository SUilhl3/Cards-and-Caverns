using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatCoin", menuName = "Create / Relic / RelicOfGreatCoin")]
public class RelicOfGreatCoin : RelicTemplate
{
    public float coinMultiplier = 1.5f;

    public override void OnBattleFinish(GameObject player)
    {
        //apply multiplier to coins to be won after battle 
        Debug.Log("applied multiplier");
    }
}
