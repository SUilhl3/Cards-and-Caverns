using UnityEngine;
using TMPro; 

public class CoinCount : MonoBehaviour
{
    public int count;
    public TMP_Text CoinCounter; 

    void Start()
    {
        count = PlayerPrefs.GetInt("amount", 0); 
        UpdateCoinDisplay();
    }

  /*  void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            count += 1;
            PlayerPrefs.SetInt("amount", count);
            UpdateCoinDisplay();
        }
    }*/

    void UpdateCoinDisplay()
    {
        if (CoinCounter != null)
        {
            CoinCounter.text = "Coins: " + count;
        }
    }
}
