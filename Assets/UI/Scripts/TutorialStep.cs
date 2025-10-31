using System.Runtime.CompilerServices;
using UnityEngine;

public enum TutorialTriggerType
{
    Manual,
    WaitForEvent
}

[CreateAssetMenu(menuName = "Tutorial/Step")]

public class TutorialStep : ScriptableObject
{
    public string stepName;
    [TextArea] public string message;
    public TutorialTriggerType triggerType;
    public string eventName;
}