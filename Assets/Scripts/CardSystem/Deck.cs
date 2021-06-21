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
        Random random = new Random();

        int length = cards.Count;
        for (int i = 0; i < length; i++)
        {
            int r = Random.Range(i, length);
            Card temp = cards[r];
            cards[r] = cards[i];
            cards[i] = temp;
        }
    }

    public virtual void Deal(CardPlace place, int count)
    {
        for (int i = 0; i < count; i++)
        {
            place.PushCard(cards[0]);
            cards.Remove(cards[0]);
        }
    }
}