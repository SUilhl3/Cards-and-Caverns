using System.Collections.Generic;
using UnityEngine;

public class DeckDisplayManager : MonoBehaviour
{
    [Header("Deck Data")]
    [SerializeField] private CardCollection playerDeck;

    [Header("UI References")]
    [SerializeField] private Transform deckContainer;
    [SerializeField] private CardSlotHandler cardSlotPrefab;

    private void Start()
    {
        DisplayDeck();
    }

    public void DisplayDeck()
    {
        foreach (Transform child in deckContainer)
            Destroy(child.gameObject);

        if (playerDeck == null || playerDeck.CardsInCollection.Count == 0)
        {
            Debug.LogWarning("No deck assigned or deck is empty.");
            return;
        }

        List<ScriptableCard> sortedDeck = playerDeck.GetSortedByElement();

        foreach (var card in sortedDeck)
        {
            CardSlotHandler slot = Instantiate(cardSlotPrefab, deckContainer);
            slot.SetCard(card);
        }
    }
}
