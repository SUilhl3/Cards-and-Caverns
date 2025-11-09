using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfImmenseHealth", menuName = "Create / Relic / RelicOfImmenseHealth")]
public class RelicOfImmenseHealth : RelicTemplate
{
    public int healthIncrease = 20;

    public override void onAcquire(PlayerCombatant player, CoinCount coinCount)
    {
        player.maxHealth += healthIncrease;
        player.currentHealth += healthIncrease;
    }
}
