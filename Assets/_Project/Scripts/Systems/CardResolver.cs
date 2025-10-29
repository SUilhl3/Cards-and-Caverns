using UnityEngine;


public static class CardResolver
{
    // Applies ScriptableCard.Effects in order
    public static void Resolve(ScriptableCard card, PlayerCombatant player, Combatant target)
    {
        if (card == null || player == null) return;
        foreach (var e in card.Effects)
        {
            switch (e.kind)
            {
                case ScriptableCard.EffectKind.Damage:
                    float baseOut = e.value;
                    // apply player's outgoing modifiers if you later add them
                    int dmg = Mathf.RoundToInt(baseOut);
                    if (target != null) target.TakeDamage(dmg);
                    break;
                case ScriptableCard.EffectKind.Block:
                    player.GainBlock(e.value);
                    break;
                case ScriptableCard.EffectKind.ApplyStatus:
                    var def = StatusLibrary.Instance.Get(e.statusId);
                    if (def == null) { Debug.LogWarning($"Unknown status {e.statusId}"); break; }
                    if (target != null) new StatusController(target).Add(def, e.value); // lightweight add
                    else new StatusController(player).Add(def, e.value);
                    break;
            }
        }
    }
}