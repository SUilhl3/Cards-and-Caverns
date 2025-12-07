using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public Button returnButton;
    public void LoadBattle()
    {
        if(returnButton != null)
        {
            returnButton.interactable = false;
        }
        SceneManager.LoadScene("BattleScene");
    }
}
