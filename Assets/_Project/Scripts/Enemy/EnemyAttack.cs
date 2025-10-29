using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Combatant target;   // Who the enemy attacks
    public int attackDamage = 5;

    private Combatant self;
    private EnemyAnimationController animController;

    void Awake()
    {
        self = GetComponent<Combatant>();
        animController = GetComponent<EnemyAnimationController>();
    }

    public void PerformAttack()
    {
        if (!target)
        {
            Debug.Log($"{self.combatantName} has no target!");
            return;
        }

        // Play the enemy's attack animation
        animController?.PlayAttack();

        // Deal damage to the target
        Debug.Log($"{self.combatantName} attacks {target.combatantName} for {attackDamage} damage!");
        target.TakeDamage(attackDamage);
    }
}
