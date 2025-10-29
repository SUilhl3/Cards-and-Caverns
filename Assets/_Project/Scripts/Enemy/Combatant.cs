using TMPro;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public string combatantName = "Enemy";
    public int maxHealth = 30;
    public int currentHealth;

    public TextMeshProUGUI healthText;

    private EnemyAnimationController _animController;

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
        _animController = GetComponent<EnemyAnimationController>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthText();

        _animController?.PlayHit(); // play hit when taking damage

        if (currentHealth <= 0)
            Die();
    }

    // Add target parameter to know who is being attacked
    public void Attack(Combatant target)
    {
        Debug.Log($"{combatantName} attacks {target.combatantName}!");

        // Play the enemy's attack animation
        _animController?.PlayAttack();

        // Deal damage to target
        target.TakeDamage(5); // placeholder
    }

    void UpdateHealthText()
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
