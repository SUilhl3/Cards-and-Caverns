using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Collection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection { get; private set; }



    public void RemoveCardFromCollection(ScriptableCard card)
    {
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            Debug.LogWarning("Card is not available in collection; cannot remove");
        }
    }

    public void AddCardToCollection(ScriptableCard card)
    {
        CardsInCollection.Add(card);
    }
}
