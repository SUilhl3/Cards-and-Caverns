using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Collection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection { get; private set; }

    public void AddCardToCollection(ScriptableCard card)
    {
        CardsInCollection.Add(card);
    }

    public void RemoveCardFromCollection(ScriptableCard card)
    {
        if (CardsInCollection.Contains(card))
            CardsInCollection.Remove(card);
        else
            Debug.LogWarning($"Card {card.name} is not in collection.");
    }

    public List<ScriptableCard> GetSortedByElement()
    {
        List<ScriptableCard> sorted = new List<ScriptableCard>(CardsInCollection);
        sorted.Sort((a, b) => a.Element.CompareTo(b.Element));
        return sorted;
    }
}