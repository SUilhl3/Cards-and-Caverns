using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttack : MonoBehaviour
{
    [Header("Basic Info")]
    public Combatant target;
    public int attackDamage = 5;

    [Header("AI Cycle")]
    public List<string> actionCycle = new List<string> { "Attack", "Charge",};

    private int currentActionIndex = 0;
    private bool isCharging = false;

    private Combatant self;
    private EnemyAnimationController animController;

    void Awake()
    {
        self = GetComponent<Combatant>();
        animController = GetComponent<EnemyAnimationController>();
        target = FindAnyObjectByType<PlayerCombatant>();
    }

    public IEnumerator TakeTurn()
    {
        string action = actionCycle[currentActionIndex];
        Debug.Log($"{self.combatantName} performs {action}");
        yield return PerformAction(action);

        // Move to next action
        currentActionIndex = (currentActionIndex + 1) % actionCycle.Count;
    }

    private IEnumerator PerformAction(string action)
    {
        switch (action)
        {
            case "Attack":
                yield return Attack();
                break;
            case "Charge":
                yield return Charge();
                break;
            case "Defend":
                yield return Defend();
                break;
            case "Poison":
                yield return Poison();
                break;
        }

        target.block = 0;
    }

    private IEnumerator Attack()
    {
        animController?.PlayAttack();
        yield return new WaitForSeconds(1f);

        int dmg = isCharging ? attackDamage * 2 : attackDamage;
        isCharging = false;

        target.TakeDamage(dmg);
        Debug.Log($"{self.combatantName} attacks {target.combatantName} for {dmg}!");
    }

    private IEnumerator Charge()
    {
        Debug.Log($"{self.combatantName} charges up!");
        animController?.PlayAttack(); //TODO no charge animation yet
        isCharging = true;
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Defend()
    {
        Debug.Log($"{self.combatantName} defends and gains block!");
        self.GainBlock(5);
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Poison()
    {
        Debug.Log($"{self.combatantName} poisons the player!");
        //TODO dont have poison animations or mechanics yet (im not sure if someone else has done this)
        yield return new WaitForSeconds(1f);
    }
}
