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
    public string cardTypeText; //leaving  as a string because I do not want to make enum or anything right now

    [Tooltip("Sprite for cards cost")]
    public string cardCost;


    [Header("Card data")]
    [Tooltip("Target the card will effect, either player or an enemy selected")]
    public GameObject target; //will likely make a unit parent class that has enemy and player derived from it
    
    [Tooltip("Energy require to use this card")]
    public int energyCost = 1;

    [Tooltip("Default damage to enemy when using this card")]
    public int damage = 7; 
    [Tooltip("Default defence gained when using this card")]
    public int defence = 5; //could have this negative to decrease block if applicable
    [Tooltip("Health recovered when using this card")]
    public int hpRecoverAmount = 0; //if negative, should damage the player

    [Tooltip("How many 'weaken' status effects to apply on enemy when using this card")]
    public int weakenAmount = 0;
    [Tooltip("How many 'vulnerable' status effects to apply on enemy when using this  card")]
    public int vulnerableAmount = 0;

    //could use additive ints to add buffs, for example:
    [Tooltip("Adds strength to player, 1 str means player adds 1 damage to all attacks")]
    public int strAddAmount = 2;
}
