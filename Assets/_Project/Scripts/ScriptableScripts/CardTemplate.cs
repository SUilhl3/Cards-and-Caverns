using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Create / Card")]
public class CardTemplate : ScriptableObject
{
    [Header("Visual Elements")]
    [Tooltip("Background for the whole card, this would be the colour of the text box")]
    public Sprite cardBg;
    [Tooltip("Card main text")]
    public string mainText;

    [Tooltip("Sprite for the card art")]
    public Sprite cardImage; 
    [Tooltip("Border for card")]
    public Sprite cardBorder;

    [Tooltip("The sprite for the card name header")]
    public Sprite cardNameHeader;
    [Tooltip("Text for the name of the card")]
    public string cardName;

    [Tooltip("The sprite for the card type header")]
    public Sprite cardTypeHeader;
    [Tooltip("Display type of card")]
    public string cardTypeText;

    [Tooltip("Sprite for cards cost")]
    public string cardCost;

    [Header("Card data")]
    [Tooltip("Energy require to use this card")]
    public int energyCost = 1;

    [Tooltip("Default damage to enemy when using this card")]
    public int damage = 7; 
    [Tooltip("Default defence gained when using this card")]
    public int defence = 5;
    [Tooltip("Health recovered when using this card")]
    public int hpRecoverAmount = 0;

}
