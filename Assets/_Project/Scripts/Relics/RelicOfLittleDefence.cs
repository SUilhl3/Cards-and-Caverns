using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfLittleDefence", menuName = "Create / Relic / RelicOfLittleDefence")]

public class RelicOfLittleDefence : RelicTemplate
{
    public int defenceBuff = 5;

    public override void OnBattleStart(PlayerCombatant player, CombatManager combatManager, EnemyManager enemyManager)
    {
        player.GainBlock(defenceBuff);
    }
}
