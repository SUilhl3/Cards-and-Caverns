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
