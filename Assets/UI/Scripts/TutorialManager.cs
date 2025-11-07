using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        [Header("Step Details")]
        public string title;
        [TextArea(2, 5)]
        public string message;

        [Header("Display Settings")]
        public Vector2 screenPosition;
        public Vector2 panelSize = new Vector2(400, 150);
    }

    [Header("UI References")]
    public RectTransform TutorialPanel;
    public TMP_Text titleText;
    public TMP_Text tutorialText;
    public Button nextButton;

    [Header("Tutorial Steps")]
    public TutorialStep[] steps;

    private int currentStep = 0;

    void Start()
    {
        Time.timeScale = 0f;
        TutorialPanel.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextStep);
        ShowStep(0);
    }

    public void ShowStep(int index)
    {
        if (index < steps.Length)
        {
            TutorialStep step = steps[index];

            TutorialPanel.gameObject.SetActive(true);

            if (titleText != null)
                titleText.text = step.title;

            tutorialText.text = step.message;

            TutorialPanel.anchoredPosition = step.screenPosition;
            TutorialPanel.sizeDelta = step.panelSize;
        }
        else
        {
            TutorialPanel.gameObject.SetActive(false);
        }
    }

    void NextStep()
    {
        currentStep++;
        ShowStep(currentStep);
    }
}
