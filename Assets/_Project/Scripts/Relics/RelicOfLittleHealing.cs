using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleHealing", menuName = "Create / Relic / RelicOfLittleHealing")]
public class RelicOfLittleHealing : RelicTemplate
{
    public int healAmt = 5;

    public override void OnBattleFinish(PlayerCombatant player, CoinCount coinCount)
    {
        int healthNeeded = player.maxHealth - player.currentHealth;

        //so current health doesn't go over max health
        if (healthNeeded <= healAmt)
        {
            player.currentHealth = player.maxHealth; 
        } else
        {
            player.currentHealth += healAmt;
        }
    }
}
