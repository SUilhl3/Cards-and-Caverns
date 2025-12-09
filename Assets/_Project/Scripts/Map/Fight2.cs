using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight2 : MonoBehaviour
{
    public Button[] returnButton;
    public int battleID;  

    public void LoadBattle()
    {
        if (!BattleProgress.instance.unlocked[battleID])
        {
            Debug.Log("Battle " + battleID + " is locked.");
            return;
        }

        for (int i = 0; i < returnButton.Length; i++)
            if (returnButton[i] != null)
                returnButton[i].interactable = false;

        SceneManager.LoadScene("BattleScene2");
    }
}
