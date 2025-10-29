using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CardData")]
public class ScriptableCard : ScriptableObject
{

    [field: SerializeField] public string CardName { get; private set; }
    [field: SerializeField, TextArea] public string CardDescription { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public CardElement Element { get; private set; }
    [field: SerializeField] public CardEffectType EffectType { get; private set; }
    [field: SerializeField] public CardRarity Rarity { get; private set; }
    [Header("Combat Params")]
    [field: SerializeField] public int Cost { get; private set; } = 1;


    public enum TargetType { None, Self, SingleEnemy, AllEnemies }
    [field: SerializeField] public TargetType Target { get; private set; } = TargetType.SingleEnemy;


    [System.Serializable]
    public enum EffectKind { Damage, Block, ApplyStatus }
    [System.Serializable]
    public class EffectData
    {
        public EffectKind kind;
        public int value = 0; // damage, block, or stacks
        public string statusId = ""; // for ApplyStatus
    }


    [Header("Effects in order")]
    [field: SerializeField] public List<EffectData> Effects { get; private set; } = new();
}

public enum CardElement
{
    Basic,
    Ice,
    Fire,
    Lightning
}

public enum CardEffectType
{
    Trap,
    Spell,
    Monster
}

public enum CardRarity
{
    Basic,
    Common,
    Rare,
    Epic,
    Legendary
}
