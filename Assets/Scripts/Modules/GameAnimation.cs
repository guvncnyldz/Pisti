using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameAnimation : MonoBehaviour
{
    public static GameAnimation instance;
    public CancellationTokenSource cancellation;
    public CancellationTokenSource cancellation2;

    [HideInInspector]
    public bool isRestart;
    public bool isEndDeck;

    private void Awake()
    {
        instance = this;
        cancellation = new CancellationTokenSource();
        cancellation2 = new CancellationTokenSource();
        isRestart = false;
    }

    public async void DealStart(Table table, List<Player> players)
    {
        cancellation = new CancellationTokenSource();

        Debug.LogWarning("Masaya kart");

        await Task.Delay(200);
        AudioManager.instance.CardDeal();

        await Task.Delay(200);
        UIManager.Instance.Animation1(table.cards[3]);

        await Task.Delay(500);
        AudioManager.instance.CardDeal();

        await Task.Delay(200);
        UIManager.Instance.Animation2(players[1], players[1].hand.cards);

        await Task.Delay(500);
        AudioManager.instance.CardDeal();

        await Task.Delay(250);
        UIManager.Instance.Animation3(players[0]);

        UIManager.Instance.MyDeckRally();
        await Task.Delay(500);
        UIManager.Instance.RotateHand();

        ClassicGame.instance.Move();

        if (isRestart)
            Application.LoadLevel((int)Scenes.Game);

        cancellation = null;
    }

    public async void Deal(List<Player> players)
    {
        cancellation = new CancellationTokenSource();
        Debug.Log("Oyunculara kart");

        await Task.Delay(250);
        AudioManager.instance.CardDeal();

        await Task.Delay(250);
        UIManager.Instance.Animation2(players[1], players[1].hand.cards);

        await Task.Delay(500);
        AudioManager.instance.CardDeal();

        await Task.Delay(250);
        UIManager.Instance.Animation3(players[0]);

        if (isEndDeck)
        {
            UIManager.Instance.deck.SetActive(false);
            isEndDeck = false;
        }

        UIManager.Instance.MyDeckRally();

        await Task.Delay(500);
        UIManager.Instance.RotateHand();

        ClassicGame.instance.Move();

        if (isRestart)
            Application.LoadLevel((int)Scenes.Game);

        cancellation = null;

    }

    public async Task PushCard(Player player, Card card)
    {

        await Task.Delay(UnityEngine.Random.Range(250, 750));

    }

    public async void TurnWin(TurnManager turnManager)
    {

        await Task.Delay(100);
        AudioManager.instance.Woosh();
        await Task.Delay(300);

        UIManager.Instance.Animation4(turnManager.lastWinner);
        await Task.Delay(500);
    }

    public async void LastWin(TurnManager turnManager)
    {

        await Task.Delay(200);
        AudioManager.instance.Woosh();
        await Task.Delay(300);

        UIManager.Instance.Animation4(turnManager.lastWinner);
    }

    public async void Pisti(TurnManager turnManager)
    {
        AudioManager.instance.Pisti();

        await Task.Delay(500);

        UIManager.Instance.Animation4(turnManager.lastWinner);
    }
}