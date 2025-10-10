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

    private List<Card> _deckPile = new();
    private List<Card> _discardPile = new();

    public List<Card> HandCards { get; private set; } = new();

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
        for (int i = 0; i < amount; i++)
        {
            if (_deckPile.Count <= 0)
            {
                _discardPile = _deckPile;
                _discardPile.Clear();
                ShuffleDeck();
            }
            if (_deckPile.Count > 0)
            {
                HandCards.Add(_deckPile[0]);
                _deckPile[0].gameObject.SetActive(true);
                _deckPile.RemoveAt(0);
            }
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

    #endregion
}
