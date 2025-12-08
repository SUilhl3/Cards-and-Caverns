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
    if (deckContainer == null || cardSlotPrefab == null)
    {
        Debug.LogError("Deck container or slot prefab is not assigned!");
        return;
    }

    // Clear previous slots
    foreach (Transform child in deckContainer)
    {
        Destroy(child.gameObject);
    }

    if (playerDeck == null || playerDeck.CardsInCollection == null || playerDeck.CardsInCollection.Count == 0)
    {
        Debug.LogWarning("No deck assigned or deck is empty.");
        return;
    }

    List<ScriptableCard> sortedDeck = playerDeck.GetSortedByElement();
    if (sortedDeck == null)
    {
        Debug.LogWarning("GetSortedByElement returned null.");
        return;
    }

    foreach (var card in sortedDeck)
    {
        if (card == null)
        {
            Debug.LogWarning("A card in the deck is null. Skipping...");
            continue;
        }

        CardSlotHandler slot = Instantiate(cardSlotPrefab, deckContainer);
        if (slot != null)
        {
            slot.SetCard(card);
        }
        else
        {
            Debug.LogWarning("Failed to instantiate card slot prefab.");
        }
    }
}
}
