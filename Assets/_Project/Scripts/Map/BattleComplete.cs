using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleComplete : MonoBehaviour
{
    public int battleID; 

    public void PlayerWon()
    {
        Debug.Log("Battle " + battleID + " completed!");

        BattleProgress.instance.CompleteBattle(battleID);

        SceneManager.LoadScene("MainMenu");
    }
}
