using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleComplete : MonoBehaviour
{
    public int battleID;

    void Start()
    {
        battleID = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Battle ID set to: " + battleID);
    }

public void PlayerWon()
{
    int id = BattleProgress.instance.currentBattleID;

    Debug.Log("Battle " + id + " completed!");

    BattleProgress.instance.CompleteBattle(id);

    SceneManager.LoadScene("levelSelect");
}

}
