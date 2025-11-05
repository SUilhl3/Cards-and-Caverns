using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
	public List<Combatant> Enemies = new();
	public Combatant FirstAlive { get { foreach (var e in Enemies) if (e != null && e.gameObject.activeSelf) return e; return null; } }
	public bool AllDefeated { get { foreach (var e in Enemies) if (e != null && e.gameObject.activeSelf) return false; return Enemies.Count > 0; } }
	void Awake()
	{
		// Ensure initial population uses the same logic as a manual Refresh
		Refresh();
	}

	// Re-scan the scene and populate the Enemies list with active non-player Combatant instances.
	// This can be called after runtime spawns to refresh the manager's list.
	public void Refresh()
	{
		// Remove null or inactive entries first
		Enemies.RemoveAll(e => e == null || !e.gameObject.activeInHierarchy);

		// Find all Combatant instances in the scene and add any non-player, active combatants to the list
		// Use the newer API when available; fall back to FindObjectsOfType behavior
	#if UNITY_2023_1_OR_NEWER
		var all = UnityEngine.Object.FindObjectsByType<Combatant>(UnityEngine.FindObjectsSortMode.None);
	#else
		var all = UnityEngine.Object.FindObjectsOfType<Combatant>();
	#endif
		foreach (var c in all)
		{
			if (c == null) continue;
			// only include active in hierarchy
			if (!c.gameObject.activeInHierarchy) continue;
			// skip player combatant (PlayerCombatant inherits Combatant)
			if (c.GetComponent<PlayerCombatant>() != null) continue;
			if (!Enemies.Contains(c)) Enemies.Add(c);
		}
	}

}
