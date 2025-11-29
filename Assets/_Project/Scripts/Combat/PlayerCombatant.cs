using UnityEngine;


public class PlayerCombatant : Combatant
{
	[Header("Energy")] public int baseEnergyPerTurn = 3; public int energy = 0;
	[Header("Draw")] public int drawPerTurn = 5;
    [Header("UI Elements")] public TMPro.TMP_Text energyUI;

    public StatusController Statuses { get; private set; }

    public void UpdateEnergyUI()
    {
        if (energyUI != null)
        {
            energyUI.text = $"{energy}";
        }
    }

	void Start() { Statuses = new StatusController(this); }
}
