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
    private List<Card> _savedDeckOrder = new();
    private List<Card> _savedDiscardOrder = new();
    public List<Card> HandCards { get; private set; } = new();


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

    public void DrawHand(int amount = 5)
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

    public void DiscardCard(Card card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            _discardPile.Add(card);
            card.gameObject.SetActive(false);
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