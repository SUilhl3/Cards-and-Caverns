using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rest: MonoBehaviour
{
    public Button[] returnButton;
    public void LoadRest()
    {
        for (int b = 0; b < returnButton.Length; b++)
        {
            if (returnButton[b] != null)
            {
                returnButton[b].interactable = false;
            }
        }
        SceneManager.LoadScene("RestScene");
    }
}
