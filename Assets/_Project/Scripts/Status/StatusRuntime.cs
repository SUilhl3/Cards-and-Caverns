using System.Collections.Generic;
using UnityEngine;


public class StatusInstance { public StatusDefinition def; public int stacks; public StatusInstance(StatusDefinition d, int s) { def = d; stacks = s; } }


public class StatusController
{
    private readonly Dictionary<string, StatusInstance> _map = new();
    private readonly Combatant _owner;
    public StatusController(Combatant owner) { _owner = owner; }


    public int GetStacks(string id) => _map.TryGetValue(id, out var i) ? i.stacks : 0;


    public void Add(StatusDefinition def, int amount)
    {
        if (amount <= 0 || def == null) return;
        if (_map.TryGetValue(def.id, out var inst))
        {
            if (!def.stackable) return;
            inst.stacks = Mathf.Clamp(inst.stacks + amount, 0, def.maxStacks);
        }
        else
        {
            _map[def.id] = new StatusInstance(def, Mathf.Clamp(amount, 0, def.maxStacks));
        }
    }
    public float ModifyIncomingDamage(float baseVal)
    {
        float v = baseVal;
        foreach (var kv in _map) v *= kv.Value.def.incomingDamageMultiplier;
        return v;
    }


    public float ModifyOutgoingDamage(float baseVal)
    {
        float v = baseVal;
        foreach (var kv in _map) v *= kv.Value.def.outgoingDamageMultiplier;
        return v;
    }


    public void OnTurnTick(StatusDefinition.TickTiming timing)
    {
        var toRemove = new List<string>();
        foreach (var kv in _map)
        {
            var inst = kv.Value; var def = inst.def;
            if (def.tickTiming != timing) continue;
            if (def.tickDamagePerStack > 0 && inst.stacks > 0)
                _owner.TakeDamage(def.tickDamagePerStack * inst.stacks);
            if (def.stacksLostPerTick > 0)
            {
                inst.stacks -= def.stacksLostPerTick;
                if (inst.stacks <= 0) toRemove.Add(def.id);
            }
        }
        foreach (var k in toRemove) _map.Remove(k);
    }
}