using System.Collections.Generic;
using UnityEngine;


public class StatusLibrary : MonoBehaviour
{
    public static StatusLibrary Instance { get; private set; }
    private readonly Dictionary<string, StatusDefinition> _byId = new();


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject); Reload();
    }


    public void Reload()
    {
        _byId.Clear();
        foreach (var s in Resources.LoadAll<StatusDefinition>("Statuses"))
            if (!string.IsNullOrEmpty(s.id)) _byId[s.id] = s;
        Debug.Log($"Statuses loaded: {_byId.Count}");
    }


    public StatusDefinition Get(string id) => _byId.TryGetValue(id, out var d) ? d : null;
}