using UnityEngine;


public class PlayerCombatant : Combatant
{
    [Header("Energy")] public int baseEnergyPerTurn = 3; public int energy = 0;
    [Header("Draw")] public int drawPerTurn = 5;


    public StatusController Statuses { get; private set; }


    void Start() { Statuses = new StatusController(this); }
}