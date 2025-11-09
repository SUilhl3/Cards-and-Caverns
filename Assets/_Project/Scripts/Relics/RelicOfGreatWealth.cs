using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatWealth", menuName = "Create / Relic / RelicOfGreatWealth")]

public class RelicOfGreatWealth : RelicTemplate
{
    public int coins = 400;

    public override void onAcquire(PlayerCombatant player, CoinCount coinCount)
    {
        coinCount.count += coins;
        
    }
}
