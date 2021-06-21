using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static void NextPlayer(ref int playerIndex)
    {
        playerIndex++;

        if (playerIndex == 2)
            playerIndex = 0;
    }

    public static int CalculateScore(List<Card> cards)
    {
        int totalScore = 0;

        foreach (Card card in cards)
        {
            totalScore += CardScore(card);
        }

        return totalScore;
    }

    private static int CardScore(Card card)
    {
        if (card.value == 1) return 1;
        if (card.value == 11) return 1;
        if (card.value == 2 && card.type == CardType.Sinek) return 2;
        if (card.value == 10 && card.type == CardType.Karo) return 3;

        return 0;
    }
}