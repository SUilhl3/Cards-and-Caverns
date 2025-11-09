using UnityEngine;

[CreateAssetMenu(fileName = "RelicOfGroupAttack", menuName = "Create / Relic / RelicOfGroupAttack")]
public class RelicOfGroupAttack : RelicTemplate
{
    public int attackAmount = 3;

    public override void OnBattleStart(PlayerCombatant player, CombatManager combatManager, EnemyManager enemyManager)
    {
        foreach (Combatant enemy in enemyManager.Enemies)
        {
            enemy.TakeDamage(attackAmount);
        }
    }
}
