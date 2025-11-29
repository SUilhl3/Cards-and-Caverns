using TMPro;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public string combatantName = "Enemy";
    public int maxHealth = 30;
    public int currentHealth;
    public int block = 0;

    public TextMeshProUGUI healthText;

    private EnemyAnimationController _animController;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        _animController = GetComponent<EnemyAnimationController>();
    }
    public void GainBlock(int amount)
    {
        block = Mathf.Max(0, block + Mathf.Max(0, amount));
    }


    public void ResetBlock() { block = 0; }

    public void TakeDamage(int amount)
    {
        int dmg = Mathf.Max(0, amount);
        if (dmg == 0) return;
        _animController?.PlayHit();
        int fromBlock = Mathf.Min(block, dmg);
        block -= fromBlock;
        int hpLoss = dmg - fromBlock;
        if (hpLoss > 0) currentHealth = Mathf.Max(0, currentHealth - hpLoss);
        UpdateHealthText();
        if (currentHealth <= 0) Die();
    }

    public void Attack(Combatant target)
    {
        Debug.Log($"{combatantName} attacks {target.combatantName}!");

        // Play the enemy's attack animation
        _animController?.PlayAttack();

        // Deal damage to target
        target.TakeDamage(5); // placeholder
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = $"{currentHealth}/{maxHealth}";
    }

    void Die()
    {
        Debug.Log($"{combatantName} has been defeated!");
        gameObject.SetActive(false);
    }
}
