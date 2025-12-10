using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rest2 : MonoBehaviour
{
    public Button[] returnButton;
    public int battleID;

    public void LoadRest()
    {
        if (battleID > BattleProgress.instance.battlesWon)
        {
            Debug.Log("Rest Area for Battle " + battleID + " is locked.");
            return;
        }

        for (int i = 0; i < returnButton.Length; i++)
            if (returnButton[i] != null)
                returnButton[i].interactable = false;

        SceneManager.LoadScene("RestScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "RestScene")
        {
            BattleProgress.instance.IncrementProgressForRestScene();
        }
    }
}
