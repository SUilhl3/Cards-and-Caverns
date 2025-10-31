using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TutorialoManager : MonoBehaviour
{
    public static TutorialoManager Instance; // so other scripts can call TriggerEvent

    [Header("References")]
    public GameObject popupUI;
    public TextMeshProUGUI messageText;
    public Button nextButton;

    [Header("Steps")]
    public TutorialStep[] steps;

    private int currentStepIndex = -1;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        popupUI.SetActive(false);
        NextStep();
    }

    public void NextStep()
    {
        currentStepIndex++;
        if (currentStepIndex >= steps.Length)
        {
            popupUI.SetActive(false);
            Debug.Log("Tutorial complete!");
            return;
        }

        var step = steps[currentStepIndex];
        popupUI.SetActive(true);
        messageText.text = step.message;

        if (step.triggerType == TutorialTriggerType.Manual)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    public void OnNextButtonClicked()
    {
        NextStep();
    }

    // Called by gameplay scripts (e.g., CardManager) when events happen
    public void TriggerEvent(string eventName)
    {
        var step = steps[currentStepIndex];
        if (step.triggerType == TutorialTriggerType.WaitForEvent && step.eventName == eventName)
        {
            NextStep();
        }
    }
}
