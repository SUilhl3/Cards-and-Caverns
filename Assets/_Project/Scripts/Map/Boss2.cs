using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    public Button[] returnButton;
    public int battleID;

    public void LoadBoss()
    {
        if (!BattleProgress.instance.unlocked[battleID])
        {
            Debug.Log("Boss battle locked.");
            return;
        }

        for (int i = 0; i < returnButton.Length; i++)
            if (returnButton[i] != null)
                returnButton[i].interactable = false;

        SceneManager.LoadScene("BossScene");
    }
}
