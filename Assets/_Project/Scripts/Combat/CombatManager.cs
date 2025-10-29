using System.Collections.Generic;
using UnityEngine;


public enum TurnState { PlayerStart, PlayerAct, PlayerEnd, EnemiesStart, EnemiesAct, EnemiesEnd, Victory, Defeat }


public class CombatManager : MonoBehaviour
{
    [Header("Links")] public PlayerCombatant player;
    public EnemyManager enemyManager;


    [Header("Deck Sources")] public ClassStartingDeck startingDeck; // optional
    public CardCollection startingDeckCollection; // alt source: TestDeck.asset
    public CardCollection allCardsLibrary; // optional: AllObtainableCards.asset for shops/rewards


    private readonly List<ScriptableCard> _draw = new();
    private readonly List<ScriptableCard> _hand = new();
    private readonly List<ScriptableCard> _discard = new();


    private TurnState _state;
    private ScriptableCard _selected;
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
        Debug.Log($"Selected {_selected.CardName}");
    }


    public void OnEnemyClicked(Combatant enemy)
    {
        if (_state != TurnState.PlayerAct || _selected == null) return;
        TryPlaySelected(enemy);
    }
    private void TryPlaySelected(Combatant target)
    {
        if (_selected == null) return;
        if (player.energy < _selected.Cost) return;
        player.energy -= _selected.Cost;
        CardResolver.Resolve(_selected, player, target);
        _hand.Remove(_selected);
        _discard.Add(_selected);
        _selected = null;
        CleanupDeaths();
        if (enemyManager.AllDefeated) { _state = TurnState.Victory; Debug.Log("Victory"); }
    }


    private void BeginBattle() { _state = TurnState.PlayerStart; StartPlayerTurn(); }


    private void StartPlayerTurn()
    {
        player.energy = player.baseEnergyPerTurn;
        player.block = 0;
        Draw(player.drawPerTurn);
        _state = TurnState.PlayerAct;
    }
    private void EndPlayerTurn()
    {
        _state = TurnState.PlayerEnd;
        DiscardHand();
        player.block = 0;
        _state = TurnState.EnemiesStart;
        EnemiesTurn();
    }


    private void EnemiesTurn()
    {
        _state = TurnState.EnemiesAct;
        foreach (var e in enemyManager.Enemies)
        {
            if (e == null || !e.gameObject.activeSelf) continue;
            var atk = e.GetComponent<EnemyAttack>();
            if (atk != null) atk.PerformAttack();
        }
        foreach (var e in enemyManager.Enemies) if (e != null) e.ResetBlock();
        if (player.currentHealth <= 0) { _state = TurnState.Defeat; Debug.Log("Defeat"); return; }
        _state = TurnState.PlayerStart; StartPlayerTurn();
    }
    private void Draw(int n)
    {
        for (int i = 0; i < n; i++)
        {
            if (_draw.Count == 0) Reshuffle();
            if (_draw.Count == 0) break;
            var c = _draw[0]; _draw.RemoveAt(0); _hand.Add(c);
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