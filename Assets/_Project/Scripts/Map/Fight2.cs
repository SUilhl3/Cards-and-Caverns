using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight2 : MonoBehaviour
{
    public Button[] returnButton;
    public void LoadBattle()
    {
        for(int b = 0;b < returnButton.Length;b++)
        {
            if (returnButton[b] != null)
            {   
                returnButton[b].interactable = false;
            }
        }
        SceneManager.LoadScene("BattleScene2");
    }
}
