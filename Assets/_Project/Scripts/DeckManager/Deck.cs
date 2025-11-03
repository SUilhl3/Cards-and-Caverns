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
    [SerializeField] private bool discardPiledEdited = false;
    [SerializeField] private bool deckPileEdited = false;

    private List<Card> _deckPile = new();
    private List<Card> _discardPile = new();
    private List<Card> _nextHandCards = new();

    public List<Card> HandCards { get; private set; } = new();


    private bool isDiscardPileVisible = false;
    private bool isDeckVisible = false;

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
        for (int i = _deckPile.Count - 1; i > 0; i--) 
        {
            int j = Random.Range(0, i + 1);
            var temp = _deckPile[i];
            _deckPile[i] = _deckPile[j];
            _deckPile[j] = temp;
        }
    }

    public void DrawHand(int amount = 5)
    {
        //for (int i = 0; i < amount; i++)
        //{
        //    if (_deckPile.Count <= 0)
        //    {
        //        _discardPile = _deckPile;
        //        _discardPile.Clear();
        //        ShuffleDeck();
        //    }
        //    if (_deckPile.Count > 0)
        //    {
        //        HandCards.Add(_deckPile[0]);
        //        _deckPile[0].gameObject.SetActive(true);
        //        _deckPile.RemoveAt(0);
        //    }
        //}

        Card cardToDraw = null;
        if(_nextHandCards.Count > 0)
        {
            cardToDraw = _nextHandCards[0];
            _nextHandCards.RemoveAt(0);
            _deckPile.Remove(cardToDraw);
        }
        else
        {
            if(_deckPile.Count == 0)
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

    public void DiscardCard(Card card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            _discardPile.Add(card);
            card.gameObject.SetActive(false);
        }
    }

    public void MoveDiscardCard(int oldIndex, int newIndex)
    {
        if(oldIndex < 0 || oldIndex >= _discardPile.Count || newIndex < 0 || newIndex >= _discardPile.Count)
        {
            return;
        }

        var card = _discardPile[oldIndex];
        _discardPile.RemoveAt(oldIndex);
        _discardPile.Insert(newIndex, card);
        discardPiledEdited = true;

    }

    public void RefillDeckFromDiscardInOrder()
    {
        _deckPile.Clear();
        _deckPile.AddRange(_discardPile);  // preserves discard pile order
        _discardPile.Clear();
    }

    public void SetNextHandEditedCards(List<Card> eligibleCards)
    {
        _nextHandCards = new List<Card>(eligibleCards);
    }

    public void DisplayDiscardPile()
    {
        foreach(Card card in _discardPile)
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
        if (isDiscardPileVisible) {HideDiscardPile();}
        else {DisplayDiscardPile();}
        isDiscardPileVisible = !isDiscardPileVisible;
    }

    public void ToggleDeckPile()
    {
        Debug.Log($"_deckPile count before display: {_deckPile.Count}");
        Debug.Log($"_discardPile count before display: {_discardPile.Count}");
        if (isDeckVisible) {HideDeckPile();}
        else { DisplayDeckPile(); }
        isDeckVisible = !isDeckVisible;
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
