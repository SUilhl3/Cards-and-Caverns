using UnityEngine;
using TMPro;

public class PlayerCombatant : Combatant
{
    [Header("Energy")]
    public int baseEnergyPerTurn = 3;
    public int energy = 0;

    [Header("Draw")]
    public int drawPerTurn = 5;

    [Header("UI Elements")]
    public TMP_Text energyUI;

    public StatusController Statuses { get; private set; }

    private PlayerAnimationController _animController;

    protected override void Awake()
    {
        base.Awake(); 
        _animController = GetComponent<PlayerAnimationController>();
        _animController?.PlayIdle(); 
    }

    public override void TakeDamage(int amount)
    {
        _animController?.PlayHit(); 
        base.TakeDamage(amount);  

        if (currentHealth <= 0)
            _animController?.PlayDie();
    }

    public override void Attack(Combatant target)
    {
        _animController?.PlayAttack(); 
        base.Attack(target);        
    }

    void Start()
    {
        Statuses = new StatusController(this);
    }

    public void UpdateEnergyUI()
    {
        if (energyUI != null)
            energyUI.text = $"{energy}";
    }
}
