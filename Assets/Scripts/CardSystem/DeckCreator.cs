using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckCreator : MonoBehaviour
{
    [SerializeField] public CardSprites cardSprites;

    public DeckCreator(CardSprites cardSprites)
    {
        this.cardSprites = cardSprites;
    }

    public List<Card> CreateDeck()
    {
        List<Card> deck = new List<Card>();

        for (int type = 1; type < 5; type++)
        {
            for (int value = 1; value <= 13; value++)
            {
                Card card = CreateCard(value, type);

                if (value == 11)
                    card.isJoker = true;

                card.isOpen = true;
                deck.Add(card);
            }
        }

        return deck;
    }

    Card CreateCard(int value, int type)
    {
        Card cardBase = new Card(value, (CardType) type);
        cardBase.SetImage(cardSprites.front[(value - 1) + ((type - 1) * 13)], cardSprites.back);
        return cardBase;
    }
}