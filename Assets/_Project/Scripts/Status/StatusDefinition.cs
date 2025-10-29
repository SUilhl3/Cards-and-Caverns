using UnityEngine;


[CreateAssetMenu(fileName = "Status", menuName = "Statuses/Status")]
public class StatusDefinition : ScriptableObject
{
    [Header("Identity")] public string id; // e.g., "Vulnerable", "Poison", "Weak"
    public string title;


    [Header("Stacking")] public bool stackable = true; public int maxStacks = 999;
    [Header("Damage Mods")] public float incomingDamageMultiplier = 1f; // Vulnerable ~1.5f
    public float outgoingDamageMultiplier = 1f; // Weak ~0.75f


    public enum TickTiming { None, StartOfTurn, EndOfTurn }
    [Header("Ticking")] public TickTiming tickTiming = TickTiming.None;
    public int tickDamagePerStack = 0; // Poison
    public int stacksLostPerTick = 0; // Poison usually 1
}