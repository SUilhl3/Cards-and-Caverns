using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public List<Combatant> Enemies = new();
    public Combatant FirstAlive { get { foreach (var e in Enemies) if (e != null && e.gameObject.activeSelf) return e; return null; } }
    public bool AllDefeated { get { foreach (var e in Enemies) if (e != null && e.gameObject.activeSelf) return false; return Enemies.Count > 0; } }
}