using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatHealing", menuName = "Create / Relic / RelicOfGreatHealing")]
public class RelicOfGreatHealing : RelicTemplate
{
    public int healAmt = 15;

    public override void OnBattleFinish(PlayerCombatant player, CoinCount coinCount)
    {
        int healthNeeded = player.maxHealth - player.currentHealth;

        //so current health doesn't go over max health
        if (healthNeeded <= healAmt)
        {
            player.currentHealth = player.maxHealth;
        }
        else
        {
            player.currentHealth += healAmt;
        }
    }
}
