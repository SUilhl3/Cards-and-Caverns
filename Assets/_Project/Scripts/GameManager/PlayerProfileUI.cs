using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerProfileUI : MonoBehaviour
{
    public TextMeshProUGUI battlesWonText;
    public TextMeshProUGUI enemiesKilledText;

    private void OnEnable()
    {
        BattleProgress.OnProgressUpdated += UpdateDisplay;
        UpdateDisplay();
    }

    private void OnDisable()
    {
        BattleProgress.OnProgressUpdated -= UpdateDisplay;
    }

    public void UpdateDisplay()
    {
        if (BattleProgress.instance == null) return;

        battlesWonText.text = "Battles Won: " + BattleProgress.instance.battlesWon;
        enemiesKilledText.text = "Enemies Killed: " + BattleProgress.instance.enemiesKilled;
    }
}
