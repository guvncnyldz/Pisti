using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : Player
{
    public async override void PushCard(Table table, PushHandler callBack, Player enemy)
    {
        Card card = CalculatePush(table, enemy);

        await GameAnimation.instance.PushCard(this, card);
        await Task.Delay(250);
        UIManager.Instance.ThrowAtCard(this, card);
        

        callBack(card);
        ClassicGame.instance.Move();
    }

    public Card CalculatePush(Table table, Player enemy)
    {
        if (table.cards?.Count > 0)
        {
            int cardsCount = table.cards.Count - 1;

            foreach (Card card in hand.cards)
            {
                if (card.value == table.cards[cardsCount].value)
                    return card;
            }

            if (table.score >= 1)
            {
                foreach (Card card in hand.cards)
                {
                    if (card.isJoker)
                        return card;
                }
            }
        }

        #region İmkansız Mod

        bool isImpossible = false;
        int random;

        random = Random.Range(0, 100);

        if (random < this.Rank)
        {
            isImpossible = true;
            Debug.Log("imkansız modda");
        }
        else
        {
            isImpossible = false;
        }

        #endregion

        Card randCard;
        int i = 0;
        bool pushCard = false;
        do
        {
            if (i == hand.cards.Count - 1)
                return hand.cards[i];

            randCard = hand.cards[i];
            pushCard = true;

            // İMKANSIZ MODSA
            if (isImpossible)
            {
                for (int j = 0; j < enemy.hand.cards.Count; j++)
                {
                    if (enemy.hand.cards[j].value == randCard.value)
                    {
                        pushCard = false;
                        i++;
                        break;
                    }
                }

                if (randCard.isJoker && pushCard)
                {
                    i++;
                    pushCard = false;
                }
            }
            else
            {
                if (randCard.isJoker)
                {
                    i++;
                    pushCard = false;
                }
            }

            
        } while (!pushCard);

        return randCard;
    }
}