using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    #region Fields and Properties

    public static Deck Instance { get; private set; }


    [SerializeField] private CardCollection _playerDeck;
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private Canvas _cardCanvas;
    [Header("Hand Layout")]
    [SerializeField] private float handSpacing = 180f; // horizontal spacing between hand cards
    [SerializeField] private float handAnchoredY = -220f; // anchored Y position for hand row
    [SerializeField] private float drawStaggerSeconds = 0.12f; // delay between draw animations
    [SerializeField] private bool discardPiledEdited = false;
    [SerializeField] private bool deckPileEdited = false;

    private List<Card> _deckPile = new();
    private List<Card> _discardPile = new();
    private List<Card> _nextHandCards = new();
    private List<Card> _savedDeckOrder = new();
    private List<Card> _savedDiscardOrder = new();
    public List<Card> HandCards { get; private set; } = new();
    // The card currently being dragged by the player (if any). Used to skip
    // repositioning the dragged card during live reflow.
    public Card CurrentlyDraggingCard { get; set; }


    private bool isDiscardPileVisible = false;
    private bool isDeckVisible = false;
    [SerializeField] private GameObject saveDeckOrder;
    [SerializeField] private GameObject saveDiscardOrder;

    #endregion

    #region Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InstantiateDeck();
    }

    private void InstantiateDeck()
    {
        for (int i = 0; i < _playerDeck.CardsInCollection.Count; i++)
        {
            Card card = Instantiate(_cardPrefab, _cardCanvas.transform);
            card.SetUp(_playerDeck.CardsInCollection[i]);
            _deckPile.Add(card);
            card.gameObject.SetActive(false);
        }

        ShuffleDeck();
    }

    private void ShuffleDeck()
    {
        if (deckPileEdited && _savedDeckOrder.Count == _deckPile.Count)
        {
            _deckPile = new List<Card>(_savedDeckOrder);
            deckPileEdited = false;
        }

        else
        {
            for (int i = _deckPile.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                var temp = _deckPile[i];
                _deckPile[i] = _deckPile[j];
                _deckPile[j] = temp;
            }
        }

    }

    public void DrawHand(int n)
    {

        Card cardToDraw = null;
        if (_nextHandCards.Count > 0)
        {
            cardToDraw = _nextHandCards[0];
            _nextHandCards.RemoveAt(0);
            _deckPile.Remove(cardToDraw);
        }
        else
        {
            if (_deckPile.Count == 0)
            {
                _deckPile.AddRange(_discardPile);
                _discardPile.Clear();
                if (!discardPiledEdited && !discardPiledEdited)
                {
                    ShuffleDeck();
                }
                discardPiledEdited = false;
                deckPileEdited = false;
            }
            if (_deckPile.Count > 0)
            {
                cardToDraw = _deckPile[0];
                _deckPile.RemoveAt(0);
            }
        }
        if (cardToDraw != null)
        {
            HandCards.Add(cardToDraw);
            cardToDraw.gameObject.SetActive(true);
        }
    }

    // Draw a specific ScriptableCard by finding its UI Card instance in deck or discard.
    // If found, the UI card is removed from its source pile, added to the hand,
    // activated, laid out and returned. Returns null if no matching UI Card is available.
    public Card DrawHand(ScriptableCard data)
    {
        if (data == null) return null;

        // Prefer exact reference equality first (fast and correct when the same ScriptableCard
        // instance is used by Deck and by the CombatManager's starting deck).
        Card found = _deckPile.Find(c => c.CardData == data);
        if (found == null)
        {
            // Fallback: try matching by card name in case the ScriptableCard references are
            // different instances (same display name) — this happens when cards are loaded
            // from different assets or duplicated in different collections.
            found = _deckPile.Find(c => c.CardData != null && c.CardData.CardName == data.CardName);
            if (found != null)
                Debug.Log($"DrawHand: matched UI card by name fallback for '{data.CardName}'");
        }

        if (found != null)
        {
            _deckPile.Remove(found);
            HandCards.Add(found);
            found.gameObject.SetActive(true);
            UpdateHandLayout();
            return found;
        }

        // If not in deck, check discard pile and move it to hand (same matching strategy)
        found = _discardPile.Find(c => c.CardData == data);
        if (found == null)
        {
            found = _discardPile.Find(c => c.CardData != null && c.CardData.CardName == data.CardName);
            if (found != null)
                Debug.Log($"DrawHand: matched UI card in discard by name fallback for '{data.CardName}'");
        }

        if (found != null)
        {
            _discardPile.Remove(found);
            HandCards.Add(found);
            found.gameObject.SetActive(true);
            UpdateHandLayout();
            return found;
        }

        return null;
    }

    // Backwards-compatible wrapper for code that still calls DrawSpecificCard.
    [System.Obsolete("Use DrawHand(ScriptableCard) instead")]
    public Card DrawSpecificCard(ScriptableCard data) => DrawHand(data);

    public void DiscardCard(Card card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            _discardPile.Add(card);
            card.gameObject.SetActive(false);
            // Reflow remaining hand cards into their new positions
            UpdateHandLayout();
        }
    }

    // Recompute target anchored positions for all cards in hand and animate them into place.
    public void UpdateHandLayout(bool immediate = false)
    {
        if (HandCards == null || HandCards.Count == 0) return;

        int total = HandCards.Count;
        float startX = -((total - 1) / 2f) * handSpacing;

        for (int i = 0; i < total; i++)
        {
            var card = HandCards[i];
            if (card == null) continue;
            // If this card is currently being dragged, skip positioning it here so
            // the drag preserves the user's pointer position.
            if (card == CurrentlyDraggingCard) continue;
            var rt = card.GetComponent<RectTransform>();
            var cm = card.GetComponent<CardMovement>();
            Vector2 target = new Vector2(startX + (i * handSpacing), handAnchoredY);

            if (cm != null)
            {
                if (immediate)
                {
                    if (rt != null) rt.anchoredPosition = target;
                }
                else
                {
                    // animate into place; stagger using index so draws look sequential
                    float delay = i * drawStaggerSeconds;
                    cm.StartCoroutine(cm.AnimateIntoHand(target, delay));
                }
            }
            else if (rt != null)
            {
                rt.anchoredPosition = target;
            }
        }
    }


    private void ApplyDiscardOrder()
    {
        if (discardPiledEdited && _savedDiscardOrder.Count == _discardPile.Count)
        {
            _discardPile = new List<Card>(_savedDiscardOrder);
        }
        else
        {
            discardPiledEdited = false;
            _savedDiscardOrder.Clear();
        }
    }

    public void RefillDeckFromDiscardInOrder()
    {
        ApplyDiscardOrder();
        _deckPile.Clear();
        _deckPile.AddRange(_discardPile);
        _discardPile.Clear();

        _savedDeckOrder = new List<Card>(_deckPile);
        deckPileEdited = true;
        discardPiledEdited = false;
    }

    public void SaveDeckOrder(List<Card> orderedCards)
    {
        if (orderedCards.Count == _deckPile.Count)
        {
            _savedDeckOrder = new List<Card>(orderedCards);
            deckPileEdited = true;
        }
        else
        {
            Debug.Log("No cards were saved...");
        }
    }



    public void DisplayDiscardPile()
    {
        ApplyDiscardOrder();
        foreach (Card card in _discardPile)
        {
            card.gameObject.SetActive(true);
        }
    }

    public void HideDiscardPile()
    {
        foreach (Card card in _discardPile)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void DisplayDeckPile()
    {
        if(discardPiledEdited && _savedDiscardOrder.Count == _deckPile.Count)
        {
            _deckPile = new List<Card>(_savedDeckOrder);
        }
        else if (deckPileEdited && _savedDeckOrder.Count == _deckPile.Count)
        {
            _deckPile = new List<Card>(_savedDeckOrder);
        }
        foreach (Card card in _deckPile)
        {
            card.gameObject.SetActive(true);
        }
    }

    public void HideDeckPile()
    {
        foreach (Card card in _deckPile)
        {
            card.gameObject.SetActive(false);
        }
    }

    public void toggleDiscardPile()
    {
        if (isDiscardPileVisible)
        {
            HideDiscardPile();
            saveDiscardOrder.SetActive(false);
        }
        else
        {
            DisplayDiscardPile();
            saveDiscardOrder.SetActive(true);
        }
        isDiscardPileVisible = !isDiscardPileVisible;
    }

    public void ToggleDeckPile()
    {
        Debug.Log($"_deckPile count before display: {_deckPile.Count}");
        Debug.Log($"_discardPile count before display: {_discardPile.Count}");
        if (isDeckVisible)
        {
            HideDeckPile();
            saveDeckOrder.SetActive(false);
        }
        else
        {
            if (deckPileEdited && _savedDeckOrder.Count == _deckPile.Count)
            {
                _deckPile = new List<Card>(_savedDeckOrder);
            }
            DisplayDeckPile();
            saveDeckOrder.SetActive(true);
        }
        isDeckVisible = !isDeckVisible;
    }

    public void SaveDiscardOrderByXPosition()
    {
        _savedDiscardOrder = new List<Card>(_discardPile);
        _savedDiscardOrder.Sort((card1, card2) => card1.transform.position.x.CompareTo(card2.transform.position.x));
        discardPiledEdited = true;
    }

    public void SaveDeckOrderByXPosition()
    {
        _savedDeckOrder = new List<Card>(_deckPile);
        _savedDeckOrder.Sort((card1, card2) => card1.transform.position.x.CompareTo(card2.transform.position.x));
        deckPileEdited = true;
    }

    public void DisplayDiscardPileButton()
    {
        toggleDiscardPile();
    }

    public void DisplayDeckButton()
    {
        ToggleDeckPile();
    }

    #endregion
}
