using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ClassStartingDeck", menuName = "StartingDecks/ClassStartingDeck")]
public class ClassStartingDeck : ScriptableObject
{
    public List<ScriptableCard> cards = new();
}


public static class StartingDeckDefaults
{
    public static List<string> IronNames() => new() { "BasicStrikeAttackCard", "BasicStrikeAttackCard", "BasicStrikeAttackCard", "BasicStrikeAttackCard", "BasicDefendBuffCard", "BasicDefendBuffCard", "BasicDefendBuffCard", "BasicDefendBuffCard", "WideSlashAttackCard" };
}