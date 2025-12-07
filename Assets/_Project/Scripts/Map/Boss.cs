using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Button returnButton;
    public void LoadBoss()
    {
        if (returnButton != null)
        {
            returnButton.interactable = false;
        }
        SceneManager.LoadScene("BossScene");
    }
}
