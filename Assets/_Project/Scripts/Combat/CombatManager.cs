using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum TurnState { PlayerStart, PlayerAct, PlayerEnd, EnemiesStart, EnemiesAct, EnemiesEnd, Victory, Defeat }


public class CombatManager : MonoBehaviour
{
	[Header("Links")] public PlayerCombatant player;
	public EnemyManager enemyManager;
	// Optional reference to the Deck UI component. If left null, Deck.Instance will be used.
	public Deck deck;


	[Header("Deck Sources")] public ClassStartingDeck startingDeck; // optional
	public CardCollection startingDeckCollection; // alt source: TestDeck.asset
	public CardCollection allCardsLibrary; // optional: AllObtainableCards.asset for shops/rewards


	private readonly List<ScriptableCard> _draw = new();
	private readonly List<ScriptableCard> _hand = new();
	private readonly List<ScriptableCard> _discard = new();


	private TurnState _state;
	private ScriptableCard _selected;
	private Card _selectedUI;

    // Track whether the upcoming player start is the very first one so we can
    // draw a full opening hand (player.drawPerTurn) and then draw 1 on subsequent turns.
    private bool _firstPlayerTurn = true;

    [SerializeField] private int drawPerTurn;

	[System.Serializable]
	public class CardCombatantEvent : UnityEvent<Card, Combatant> { }

	// Fires when a card is successfully played (uiCard, target)
	public CardCombatantEvent OnCardPlayed = new CardCombatantEvent();

	void Start()
	{
		if (player == null || enemyManager == null) { Debug.LogError("CombatManager missing refs"); enabled = false; return; }
		BuildStartingDeck();
		Shuffle(_draw);
		BeginBattle();
	}


	void Update()
	{
		if (_state == TurnState.PlayerAct)
		{
			if (Input.GetKeyDown(KeyCode.Return)) EndPlayerTurn();
		}
	}


	public void OnCardClicked(Card card) // hook this from your Card UI click
	{
		if (_state != TurnState.PlayerAct) return;
		if (card == null || card.CardData == null) return;
		if (player.energy < card.CardData.Cost) { Debug.Log("Not enough energy"); return; }
		_selected = card.CardData;
		_selectedUI = card;
		Debug.Log($"Selected {_selected.CardName}");
	}


	public void OnEnemyClicked(Combatant enemy)
	{
		if (_state != TurnState.PlayerAct || _selected == null) return;
		// When using click-to-select + click-to-target flow, use PlayCard so PlayCard's return and event are used
		var played = PlayCard(_selectedUI, enemy);
		if (played && _selectedUI != null && Deck.Instance != null) Deck.Instance.DiscardCard(_selectedUI);
		_selectedUI = null;
	}
	// Try to play the currently-selected ScriptableCard on the given target.
	// Returns true if the play succeeded (energy available and card resolved), false otherwise.
	// Try to play the currently-selected ScriptableCard on the given target.
	// If uiCard is supplied, the UI card will be discarded from the Deck on successful play.
	// Returns true if the play succeeded (energy available and card resolved), false otherwise.
	private bool TryPlaySelected(Combatant target)
	{
		if (_selected == null) return false;
		if (player.energy < _selected.Cost) return false;
		player.energy -= _selected.Cost;
		CardResolver.Resolve(_selected, player, target);
		_hand.Remove(_selected);
		_discard.Add(_selected);
		_selected = null;
		CleanupDeaths();
		if (enemyManager.AllDefeated) { _state = TurnState.Victory; Debug.Log("Victory"); }

		return true;
	}

	// Centralized API for playing a UI card on a target combatant.
	// This method will set the selected ScriptableCard, attempt to play it using TryPlaySelected,
	// and if successful will also tell the Deck to discard the UI card so the UI state remains in sync.
	public bool PlayCard(Card uiCard, Combatant target)
	{
		if (_state != TurnState.PlayerAct) return false;
		if (uiCard == null || uiCard.CardData == null) return false;

		_selected = uiCard.CardData;
		var played = TryPlaySelected(target);
		if (played)
		{
			// Fire the event so listeners (UI, SFX) can react to a successful play
			OnCardPlayed?.Invoke(uiCard, target);
			return true;
		}

		// If play failed (e.g., not enough energy), clear the selection so it doesn't linger
		_selected = null;
		return false;
	}


	private void BeginBattle() { _state = TurnState.PlayerStart; StartPlayerTurn(); }


	private void StartPlayerTurn()
    {
        print("Starting Player Turn");
		player.energy = player.baseEnergyPerTurn;
		player.block = 0;
        // Draw opening hand of `player.drawPerTurn` on the very first player turn,
        // otherwise draw 1 card per turn.
        int drawAmount = _firstPlayerTurn ? player.drawPerTurn : 1;
		Draw(drawAmount);
		_firstPlayerTurn = false;
        _state = TurnState.PlayerAct;
	}
	private void EndPlayerTurn()
	{
		_state = TurnState.PlayerEnd;
        DiscardHand();
		//player.block = 0;
        print("Ending Player Turn");
		_state = TurnState.EnemiesStart;
        EnemiesTurn();
	}


	private void EnemiesTurn()
    {
        print("Enemies' Turn");
		_state = TurnState.EnemiesAct;
		foreach (var e in enemyManager.Enemies)
		{
			if (e == null || !e.gameObject.activeSelf) continue;
			var atk = e.GetComponent<EnemyAttack>();
			if (atk != null) StartCoroutine(atk.TakeTurn());
		}
		foreach (var e in enemyManager.Enemies) if (e != null) e.ResetBlock();
		if (player.currentHealth <= 0) { _state = TurnState.Defeat; Debug.Log("Defeat"); return; }
		_state = TurnState.PlayerStart; StartPlayerTurn();
        print("Ending Enemies' Turn");
	}
	private void Draw(int n)
	{
		for (int i = 0; i < n; i++)
		{
			if (_draw.Count == 0) Reshuffle();
			if (_draw.Count == 0) break;
			var c = _draw[0]; _draw.RemoveAt(0); _hand.Add(c);

			// Try to activate the corresponding UI Card in the Deck UI.
			// Prefer an explicitly assigned `deck` reference; otherwise, use the singleton Deck.Instance.
			Deck deckRef = deck != null ? deck : Deck.Instance;
			if (deckRef != null)
			{
				var uiCard = deckRef.DrawHand(c);
				if (uiCard == null)
					Debug.Log($"No UI Card found for ScriptableCard '{c.CardName}' (this is informational; the draw will still occur logically)");
			}
			else
			{
				// If no Deck UI is present, just log — game logic (hand lists) still updated.
				Debug.Log($"Drew logical card '{c.CardName}' (no Deck UI available)");
			}
			// TODO: spawn a Card view using your Card prefab if desired
		}
	}


	private void DiscardHand() { _discard.AddRange(_hand); _hand.Clear(); }


	private void Reshuffle() { _draw.AddRange(_discard); _discard.Clear(); Shuffle(_draw); }


	private static void Shuffle<T>(List<T> list) { for (int i = 0; i < list.Count; i++) { int j = Random.Range(i, list.Count); (list[i], list[j]) = (list[j], list[i]); } }


	private void CleanupDeaths() { /* Enemy objects disable themselves in Die(); nothing to do */ }
	private void BuildStartingDeck()
	{
		_draw.Clear(); _hand.Clear(); _discard.Clear();
		if (startingDeck != null && startingDeck.cards != null && startingDeck.cards.Count > 0)
			_draw.AddRange(startingDeck.cards);
		else if (startingDeckCollection != null && startingDeckCollection.CardsInCollection != null)
			_draw.AddRange(startingDeckCollection.CardsInCollection);
		else
		{
			// fallback by names using CardLibrary
			foreach (var name in StartingDeckDefaults.IronNames())
			{
				var c = CardLibrary.Instance?.FindByName(name);
				if (c != null) _draw.Add(c); else Debug.LogWarning($"Missing card by name: {name}");
			}
		}
	}

}
