using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public Button returnButton;
    public int battleID; 

    public void LoadBattle()
    {
        if (!BattleProgress.instance.unlocked[battleID])
        {
            Debug.Log("Battle " + battleID + " is locked.");
            return;
        }

        if (returnButton != null)
            returnButton.interactable = false;

        SceneManager.LoadScene("BattleScene");
    }
}
