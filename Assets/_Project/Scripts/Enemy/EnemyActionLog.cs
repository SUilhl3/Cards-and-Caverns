using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class EnemyActionLog : MonoBehaviour
{
    public static EnemyActionLog Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text logText;

    [Header("Settings")]
    [SerializeField] private int maxEntries = 5;

    private readonly LinkedList<string> _entries = new();

    void Awake()
    {
        Instance = this;
        if (logText == null)
        {
            logText = GetComponentInChildren<TMP_Text>();
        }
        RefreshText();
    }

    public void LogAction(string message)
    {
        if (string.IsNullOrEmpty(message)) return;
        _entries.AddFirst(message);
        while (_entries.Count > maxEntries) _entries.RemoveLast();
        RefreshText();
    }

    public void Clear()
    {
        _entries.Clear();
        RefreshText();
    }

    private void RefreshText()
    {
        if (logText == null) return;
        var sb = new StringBuilder();
        foreach (var e in _entries) sb.AppendLine(e);
        logText.text = sb.ToString();
    }
}
