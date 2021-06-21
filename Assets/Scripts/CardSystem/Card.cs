using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Card : MonoBehaviour
{
    public CardType type;
    public int value;

    public Sprite imageFront;
    public Sprite imageBack;

    public bool isJoker;
    public bool isOpen;

    public bool IsPushable
    {
        get { return IsPushable; }
        set { GetComponent<Button>().enabled = value; }
    }

    public Card(int value, CardType type)
    {
        this.value = value;
        this.type = type;

        if (value == 11)
            isJoker = true;
    }

    public void SetImage(Sprite imageFront, Sprite imageBack)
    {
        this.imageFront = imageFront;
        this.imageBack = imageBack;
    }


    public void FlipCard(bool isCardClose)
    {
        if (!isCardClose)
        {
            GetComponent<Image>().sprite = imageBack;
        }
        else
        {
            GetComponent<Image>().sprite = imageFront;
        }
    }

    public bool Compare(Card card)
    {
        bool isValueEqual = card.value == value;
        bool isTypeEqual = card.type == type;

        return isTypeEqual && isValueEqual;
    }

    public bool Compare(int value)
    {
        return this.value == value;
    }

    public bool Compare(CardType trumpType)
    {
        return type == trumpType;
    }

    public Card Clone()
    {
        return (Card) this.MemberwiseClone();
    }
}