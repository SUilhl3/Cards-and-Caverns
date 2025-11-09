using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGreatDefence", menuName = "Create / Relic / RelicOfGreatDefence")]

public class RelicOfGreatDefence : RelicTemplate
{
    public int defenceBuff = 15;

    public override void OnBattleStart(PlayerCombatant player, CombatManager combatManager, EnemyManager enemyManager)
    {
        player.GainBlock(defenceBuff);
    }
}
