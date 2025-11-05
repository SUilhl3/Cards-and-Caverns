using UnityEngine;
using UnityEngine.UI;

public class CardSlotHandler : MonoBehaviour
{
    [SerializeField] private Image cardImage;

    private ScriptableCard cardData;

    public void SetCard(ScriptableCard newCard)
    {
        cardData = newCard;

        if (cardData != null && cardData.Image != null)
        {
            cardImage.sprite = cardData.Image;
            cardImage.gameObject.SetActive(true);
        }
        else
        {
            cardImage.sprite = null;
            cardImage.gameObject.SetActive(false);
        }
    }

    private void Reset()
    {
        if (cardImage == null)
            cardImage = GetComponentInChildren<Image>();
    }
}
