using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGoodHealth", menuName = "Create / Relic / RelicOfGoodHealth")]

public class RelicOfGoodHealth : RelicTemplate
{
    public int healthIncrease = 10;

    public override void onAcquire(PlayerCombatant player, CoinCount coinCount)
    {
        player.maxHealth += healthIncrease;
        player.currentHealth += healthIncrease;
    }
}
