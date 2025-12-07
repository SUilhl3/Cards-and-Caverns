using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rest2: MonoBehaviour
{
    public Button returnButton;
    public void LoadRestSolo()
    {
        if (returnButton != null)
        {
            returnButton.interactable = false;
        }
        SceneManager.LoadScene("RestScene");
    }
}
