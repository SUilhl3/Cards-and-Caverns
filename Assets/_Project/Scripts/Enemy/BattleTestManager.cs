using UnityEngine;
using System.Collections;

public class BattleTestManager : MonoBehaviour
{
    public Combatant player;
    public EnemyAttack[] enemies;


    void Update()
    {
        // Currently ends turn manually for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EnemyTurnSequence());
        }

        // click to attack enemy
        if (Input.GetMouseButtonDown(0))
        {
            TryClickDamage2D(10);
        }
    }

    private void TryClickDamage2D(int damage)
    {
        var cam = Camera.main;
        if (cam == null) return;

        Vector3 worldPoint3 = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldPoint = new Vector2(worldPoint3.x, worldPoint3.y);

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit != null)
        {
            var combatant = hit.GetComponent<Combatant>();
            if (combatant != null)
            {
                Debug.Log($"{player.combatantName} attacks {combatant.combatantName}!");
                combatant.TakeDamage(damage);
            }
        }
    }

    private IEnumerator EnemyTurnSequence()
    {
        Debug.Log(" --------- Enemy Turn ---------");

        foreach (var enemy in enemies)
        {
            if (enemy == null || enemy.GetComponent<Combatant>().currentHealth <= 0)
                continue;

            yield return enemy.TakeTurn();
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("--------- Player Turn ---------");
    }
}