using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Button returnButton;
    public int battleID;

    public void LoadBoss()
    {
        if (battleID > BattleProgress.instance.battlesWon)
        {
            Debug.Log("Boss battle locked.");
            return;
        }

        if (returnButton != null)
            returnButton.interactable = false;

        SceneManager.LoadScene("BossScene");
    }
}
