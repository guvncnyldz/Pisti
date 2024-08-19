using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> cards;

    public void NewDeck()
    {
        cards = new List<Card>();

        DeckCreator deckCreator = GetComponent<DeckCreator>();
        cards = deckCreator.CreateDeck();
    }
    public void Shuffle()
    {
        int length = cards.Count;
        for (int i = 0; i < length; i++)
        {
            int r = Random.Range(i, length);
            Card temp = cards[r];
            cards[r] = cards[i];
            cards[i] = temp;
        }
    }

    public virtual void Deal(CardPlace place, int count, bool noJoker = false)
    {
        int cardNumber = 0;
        int selectedCard = 0;
        
        do
        {
            if (noJoker && cards[cardNumber].isJoker)
            {
                cardNumber++;
                continue;
            }

            place.PushCard(cards[cardNumber]);
            cards.Remove(cards[cardNumber]);
            cardNumber = 0;
            selectedCard++;
        } while (selectedCard < count);

        /*
        for (int i = 0; i < count; i++)
        {
            if (noJoker && cards[0].isJoker)
            {
                i--;
                continue;
            }

            place.PushCard(cards[cardNumber]);
            cards.Remove(cards[cardNumber]);
        }*/
    }
}