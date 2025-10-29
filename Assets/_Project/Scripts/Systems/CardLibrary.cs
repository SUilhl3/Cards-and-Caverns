using System.Collections.Generic;
using UnityEngine;


public class CardLibrary : MonoBehaviour
{
    public static CardLibrary Instance { get; private set; }
    public static CardLibrary instance => Instance;


    [Header("Primary Source of Truth")]
    public CardCollection allCardsLibrary; // assign AllObtainableCards.asset if available


    private readonly List<ScriptableCard> _all = new();
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); Reload();
    }


    public void Reload()
    {
        _all.Clear();
        if (allCardsLibrary != null && allCardsLibrary.CardsInCollection != null)
            _all.AddRange(allCardsLibrary.CardsInCollection);
        else
            _all.AddRange(Resources.LoadAll<ScriptableCard>("Cards"));
        Debug.Log($"Cards loaded: {_all.Count}");
    }


    public ScriptableCard FindByName(string cardName)
    {
        foreach (var c in _all) if (c != null && c.CardName == cardName) return c; return null;
    }


    public IEnumerable<ScriptableCard> AllCards => _all;
}