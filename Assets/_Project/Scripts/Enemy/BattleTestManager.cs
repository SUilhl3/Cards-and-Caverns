using UnityEngine;

public class BattleTestManager : MonoBehaviour
{
    public Combatant player;
    public EnemyAttack[] enemies;

    void Update()
    {
        // Enemy 0 attacks player
        if (Input.GetKeyDown(KeyCode.Alpha1))
            enemies[0].PerformAttack();

        // Enemy 1 attacks player
        if (Input.GetKeyDown(KeyCode.Alpha2))
            enemies[1].PerformAttack();

        // Player attacks enemy 0
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"{player.combatantName} attacks {enemies[0].GetComponent<Combatant>().combatantName}!");
            enemies[0].GetComponent<Combatant>().TakeDamage(10);
        }

        // Test enemy hit manually
        if (Input.GetKeyDown(KeyCode.H))
        {
            enemies[0].GetComponent<Combatant>().TakeDamage(5);
            enemies[1].GetComponent<Combatant>().TakeDamage(5);
        }
    }
}
