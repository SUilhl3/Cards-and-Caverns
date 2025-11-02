using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POE_Mini_Boss : MonoBehaviour
{
    public void OnOptionButtonClicked(string buttonId, List<string> siblingButtonIds)
    {
        // Save chosen button and siblings
        PlayerPrefs.SetString("ChosenButton", buttonId);
        PlayerPrefs.SetString("SiblingButtons", string.Join(",", siblingButtonIds));
        PlayerPrefs.SetInt("OptionChosen", 1); // Flag that choice made

        PlayerPrefs.Save();

        // Load event scene
        //SceneManager.LoadScene(""); Change this when we have the scenes ready
        Debug.Log("Loading Mini Boss Scene...");

    }
    void Start()
    {
        if (PlayerPrefs.GetInt("OptionChosen", 0) == 1)
        {
            string chosenButton = PlayerPrefs.GetString("ChosenButton");
            string siblingButtonsCSV = PlayerPrefs.GetString("SiblingButtons");
            List<string> siblingButtons = new List<string>(siblingButtonsCSV.Split(','));

            // Disable the chosen button and siblings
            DisableButtonById(chosenButton);
            foreach (var id in siblingButtons)
            {
                DisableButtonById(id);
            }
        }
    }

    void DisableButtonById(string buttonId)
    {
        // Find button in scene by name or stored reference and disable interactability
        Button btn = GameObject.Find(buttonId)?.GetComponent<Button>();
        if (btn != null)
        {
            btn.interactable = false;
        }
    }
}
