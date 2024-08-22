using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TurnManager
{
    private List<Player> players;

    public Card biggestCard;
    public Player lastWinner;
    public Table table;

    private bool trumpActive = false;

    private int currentTurn;
    private int startTurn = 0;
    public int totalTurn = 0;

    public TurnManager(List<Player> players)
    {
        this.players = players;

        table = new Table();
        biggestCard = new Card(0, 0);

        startTurn = Random.Range(0,1);
        currentTurn = startTurn;
    }

    public void NextRound()
    {
        this.players = players;

        table = new Table();
        biggestCard = new Card(0, 0);

        GameUtils.NextPlayer(ref startTurn);
        currentTurn = startTurn;
    }

    public void BeginTurn()
    {
        totalTurn++;

        // İMKANSIZ MOD İÇİN RAKİBİ DE YOLLUYORUZ
        int enemyIndex;
        if (currentTurn == 1)
        {
            enemyIndex = 0;
        }
        else
        {
            enemyIndex = 1;
        }

        players[currentTurn].PushCard(table, EndTurn, players[enemyIndex]);
    }

    public void EndTurn(Card card)
    {
        players[currentTurn].hand.PullCard(card);

        if (table.cards.Count == 1 && card.Compare(table.GetLastCard().value))
        {
            lastWinner = players[currentTurn];
            Pisti(card);
        }
        else if (table.cards.Count > 0 && (card.Compare(table.GetLastCard().value) || card.value == 11))
        {
            lastWinner = players[currentTurn];
            GiveScore(card);
        }
        else
        {
            table.PushCard(card);
        }

        GameUtils.NextPlayer(ref currentTurn);
    }


    void Pisti(Card card)
    {
        table.PushCard(card);

        players[currentTurn].CurrentScore += table.score;
        players[currentTurn].scoreCardCount += table.cards.Count;

        table.ClearCards();

        players[currentTurn].CurrentScore += 10;
        players[currentTurn].pistiCount += 1;


        GameAnimation.instance.Pisti(this);
    }

    public void GiveScore(Card card)
    {
        table.PushCard(card);

        players[currentTurn].CurrentScore += table.score;
        players[currentTurn].scoreCardCount += table.cards.Count;

        ClassicGame.instance.firstTableClear++;
        table.ClearCards();

        // EĞER KAZANAN OYUNCU İSE VE İLK TURSA
        if (this.lastWinner as User != null && ClassicGame.instance.firstTableClear == 1)
        {
            ClassicGame.instance.FirstThreeCard();
        }
        GameAnimation.instance.TurnWin(this);
        
    }

    public void GiveLastScore()
    {
        lastWinner.CurrentScore += table.score;
        players[currentTurn].scoreCardCount += table.cards.Count;
        table.ClearCards();

        Player biggestPlayer = players[0];

        if (biggestPlayer.scoreCardCount < players[1].scoreCardCount)
            biggestPlayer = players[1];

        biggestPlayer.CurrentScore += 3;

        GameAnimation.instance.LastWin(this);

    }
}