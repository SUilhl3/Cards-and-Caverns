using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight2 : MonoBehaviour
{
    public Button returnButton;
    public int battleID;

    public void LoadBattle()
    {
        if (battleID > BattleProgress.instance.battlesWon)
        {
            Debug.Log("Battle " + battleID + " is locked.");
            return;
        }
        BattleProgress.instance.currentBattleID = battleID;

        if (returnButton != null)
            returnButton.interactable = false;

        SceneManager.LoadScene("BattleScene");
    }
}
