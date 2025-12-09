using UnityEngine;
using System.Collections.Generic;

public class Potion : MonoBehaviour
{
    public int potionAmount = 1;
    public int healAmount = 10;
    public PlayerCombatant player;

    void Awake()
    {
        player = FindFirstObjectByType<PlayerCombatant>().GetComponent<PlayerCombatant>();
    }

    public void potionClicked()
    {
        if(player.currentHealth == player.maxHealth){return;}
        if(potionAmount >= 1) { usePotion(); --potionAmount; }
        else{Debug.Log("No potions remain");}
    }

    public void usePotion()
    {
        player.HealHealth(healAmount);
    }
}
