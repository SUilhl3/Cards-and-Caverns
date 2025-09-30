using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    [Header("Visual Elements")]
    public Image cardBg;
    public TextMeshProUGUI mainText;

    public Image cardImage;
    public Image cardBorder;

    public Image cardNameHeader;
    public TextMeshProUGUI cardName;

    public Image cardTypeHeader;
    public TextMeshProUGUI cardTypeText;

    public TextMeshProUGUI cardCost;

    [Header("Card Template")]
    [Tooltip("Put the scriptable object of the card data here")]
    public CardTemplate cardTemplate;

    void Start()
    {
        //there is a card template, then assign all of the values to what's in the scriptable object
        if (cardTemplate != null)
        {
            // Sprites
            cardBg.sprite = cardTemplate.cardBg;
            cardImage.sprite = cardTemplate.cardImage;
            cardBorder.sprite = cardTemplate.cardBorder;
            cardNameHeader.sprite = cardTemplate.cardNameHeader;
            cardTypeHeader.sprite = cardTemplate.cardTypeHeader;

            // Text
            mainText.text = cardTemplate.mainText;
            cardName.text = cardTemplate.cardName;
            cardTypeText.text = cardTemplate.cardTypeText;
            cardCost.text = cardTemplate.cardCost;
        }
    }
}
