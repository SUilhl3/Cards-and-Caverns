using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rest2 : MonoBehaviour
{
    public Button[] returnButton;
    public int battleID;

    public void LoadRest()
    {
        if (!BattleProgress.instance.unlocked[battleID])
        {
            Debug.Log("Rest Area locked.");
            return;
        }

        for (int i = 0; i < returnButton.Length; i++)
            if (returnButton[i] != null)
                returnButton[i].interactable = false;

        SceneManager.LoadScene("RestScene");
    }
}
