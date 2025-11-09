using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfQuickDraw", menuName = "Create / Relic / RelicOfQuickDraw")]
public class RelicOfQuickDraw : RelicTemplate
{
    public int numExtraCards = 1;

    public override void OnBattleStart(PlayerCombatant player, CombatManager combatManager, EnemyManager enemyManager)
    {
        player.drawPerTurn += numExtraCards;

    }
}
