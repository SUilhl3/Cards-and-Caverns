using TMPro;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public string combatantName = "Enemy";
    public int maxHealth = 30;
    public int currentHealth;
    public int block = 0;

    public TextMeshProUGUI healthText;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void GainBlock(int amount)
    {
        block = Mathf.Max(0, block + Mathf.Max(0, amount));
    }

    public void ResetBlock() { block = 0; }

    public virtual void TakeDamage(int amount)
    {
        int dmg = Mathf.Max(0, amount);
        if (dmg == 0) return;

        int fromBlock = Mathf.Min(block, dmg);
        block -= fromBlock;
        int hpLoss = dmg - fromBlock;
        if (hpLoss > 0)
            currentHealth = Mathf.Max(0, currentHealth - hpLoss);

        UpdateHealthText();

        if (currentHealth <= 0)
            Die(); 
    }

    public virtual void Attack(Combatant target)
    {
        Debug.Log($"{combatantName} attacks {target.combatantName}!");
        // Deal damage to target
        target.TakeDamage(5); 
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
