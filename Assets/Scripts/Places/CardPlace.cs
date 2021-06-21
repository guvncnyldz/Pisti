using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[Serializable]
public abstract class CardPlace
{
    public List<Card> cards;

    public int score = 0;

    public virtual void PushCard(Card card)
    {
        if (cards == null)
            cards = new List<Card>();


        cards.Add(card);
        ScoreCalculater();
    }

    public virtual void PullCard(Card card)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].value == card.value && cards[i].type == card.type)
            {
                cards.RemoveAt(i);
                break;
            }
        }
    }

    public Card GetLastCard()
    {
        if (cards.Count > 0)
            return cards[cards.Count - 1];

        return new Card(0, 0);
    }

    public virtual Card PullCard(int index)
    {
        Card card = cards[index];
        cards.Remove(card);

        return card;
    }

    public virtual void ClearCards()
    {
        cards?.Clear();
        score = 0;
    }

    public void ScoreCalculater()
    {
        score = GameUtils.CalculateScore(cards);
    }
}