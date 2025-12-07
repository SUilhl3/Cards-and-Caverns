using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class POE_Boss : MonoBehaviour
{
    [SerializeField] private Button BossButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetInt("EventCompleted",0) == 1)
        {
            BossButton.enabled = false;
        }
    }

    public void LoadFightScene()
    {
        //SceneManager.LoadScene(""); Change when needed
        Debug.Log("Loading Boss Battle");
    }

    public void NoMoreClick()
    {
        BossButton.interactable = false;
        PlayerPrefs.SetInt("EventCompleted", 1);
        PlayerPrefs.Save();
    }
}
