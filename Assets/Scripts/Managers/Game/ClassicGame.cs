using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ClassicGame : MonoBehaviour
{
    public static ClassicGame instance;

    [SerializeField] private AdManagerInterstitial adManagerInterstitial;

    [Header("Havuz Puanı")]
    public int havuzPoint = 101;

    public List<Player> players;
    public List<Card> threeCards;

    [HideInInspector]
    public int turn, firstTableClear;
    public int[] gameScore = { 0, 0 };
    public int[] currentScore = { 0, 0 };

    private Deck deck;
    private TurnManager turnManager;

    private bool isGameStart;
    private bool endGame;

    private int maxScore = 30;
    private int gameCount;  // Reklam için kaç oyun kaldığı


    public void Start()
    {
        gameCount = PlayerPrefs.GetInt("GameCount", 0);

        //adManagerInterstitial = FindObjectOfType<AdManagerInterstitial>();
        //adManagerBanner = FindObjectOfType<AdManagerBanner>();

        //adManagerInterstitial.RequestInterstitial();
        AdManagerInterstitial.instance.RequestInterstitial();
        AdManagerBanner.instance.HideBanner();

        deck = FindObjectOfType<Deck>();
        turnManager = new TurnManager(players);

        turn = 1;
        endGame = false;
        firstTableClear = 0;

        Move();
    }

    public void IlkUcKart()
    {
        UIManager.Instance.OpenThreeCardPanel();
    }

    public void Move()
    {
        if (!isGameStart)
        {
            Round();
            return;
        }

        if (turnManager.totalTurn > 7)
        {
            if (deck.cards.Count > 0)
            {
                DealDeckToPlayer();
                GameAnimation.instance.Deal(players);
                //Debug.LogError("card count : " + deck.cards.Count);
                if (deck.cards.Count <= 0)
                {
                    GameAnimation.instance.isEndDeck = true;
                    //Debug.LogError("HELAL KANKA");
                    //UIManager.Instance.deck.SetActive(false);
                    //UIManager.Instance.deck.transform.position = new Vector3(UIManager.Instance.deck.transform.position.x - 100, UIManager.Instance.deck.transform.position.y, UIManager.Instance.deck.transform.position.z);
                }
            }
            else
            {
                // 52 KART BİTMİŞ
                turnManager.GiveLastScore();
                EndDeck();
                Round();
            }

            return;
        }
        turnManager.BeginTurn();
    }

    void Round()
    {
        CalculateTotalScore();
        Player winner = AnyPlayerWin();

        if (turnManager.totalTurn > 0)
        {
            //adManagerInterstitial.interstitial.Show();
            //Debug.LogError("DAFUQ IS THIS");
        }

        if (turn == 1)
        {
            FirstRound();
        }
        if (winner)
        {
            Reklam();
            Win(winner);
        }
        else
        {
            Reklam();
            //StartGame();
        }
    }

    void Reklam()
    {
        //adManagerInterstitial.ShowInterstitialAD();
        gameCount++;
        PlayerPrefs.SetInt("GameCount", gameCount);
        if (gameCount > 1)
        {
            gameCount = 0;
            PlayerPrefs.SetInt("GameCount", gameCount);
            AdManagerInterstitial.instance.ShowInterstitialAD();
            //adManagerInterstitial.ShowInterstitialAD();
        }
    }

    public Player AnyPlayerWin()
    {
        Player turnWiner = players[0];

        foreach (Player player in players)
        {
            if (player.TotalScore > turnWiner.TotalScore)
                turnWiner = player;
        }

        if (turnWiner.TotalScore > maxScore)
        {
            return turnWiner;
        }

        return null;
    }

    public void StartGame()
    {
        //Debug.LogError(firstTableClear);
        UIManager.Instance.deck.SetActive(true);
        firstTableClear = 0;
        turnManager.NextRound();
        deck.NewDeck();
        deck.Shuffle();
        DealDeckToTable();
        DealDeckToPlayer();
        DisplayTable();

        GameAnimation.instance.DealStart(turnManager.table, players);

        isGameStart = true;
    }

    void DealDeckToTable()
    {
        deck.Deal(turnManager.table, 4);

        for (int i = 0; i < 3; i++)
        {
            turnManager.table.cards[i].isOpen = false;
            threeCards[i].isOpen = true;

            threeCards[i].value = turnManager.table.cards[i].value;
            threeCards[i].type = turnManager.table.cards[i].type;

            threeCards[i].SetImage(GetComponent<DeckCreator>().cardSprites.front[threeCards[i].value - 1 + ((int)threeCards[i].type - 1) * 13], GetComponent<DeckCreator>().cardSprites.back);
            threeCards[i].GetComponent<Image>().sprite = GetComponent<DeckCreator>().cardSprites.front[threeCards[i].value - 1 + ((int)threeCards[i].type - 1) * 13];
        }


        turnManager.totalTurn = 0;
    }

    void DealDeckToPlayer()
    {
        foreach (Player player in players)
        {
            player.hand.ClearCards();
            deck.Deal(player.hand, 4);
        }

        turnManager.totalTurn = 0;
    }

    void EndDeck()
    {
        int i = 0;
        foreach (var VARIABLE in players)
        {
            currentScore[i] = players[i].CurrentScore;
            i++;
        }

    }

    void CalculateTotalScore()
    {
        foreach (var VARIABLE in players)
        {
            VARIABLE.CalculateTotalScore();
            Debug.Log("Calculate 2 " + VARIABLE.gameObject.tag + " toplam skoru " + VARIABLE.TotalScore + " ve won game =" + VARIABLE.WonGameScore);
        }

        CheckScore();
    }

    void CheckScore()
    {
        //Debug.LogError("BOT = " + currentScore[0] + " -- Player = " + currentScore[1]);
        //Debug.LogError("baştaki score 1 = " + gameScore[0] + " - - score 2 = " + gameScore[1]);
        int i = 0;

        foreach (var VARIABLE in players)
        {
            if (players[i].TotalScore >= havuzPoint)
                gameScore[i] = players[i].TotalScore;
            i++;
        }
        //Debug.LogError("ikinci score 1 = " + gameScore[0] + " - - score 2 = " + gameScore[1]);

        int maxScore = Mathf.Max(gameScore[0], gameScore[1]);

        // PUANLAR EŞİTSE MAX SKORU BOTA EŞİTLİYOR
        if (maxScore == 0 && (gameScore[0] != 0 || gameScore[1] != 0))
        {
            maxScore = gameScore[0];
        }

        //Debug.LogError("max score= " + maxScore);
        //Debug.LogError("turn = " + turn);

        if (maxScore != 0)
        {
            if (maxScore == players[0].TotalScore)
            {
                players[0].WonGameScore++;
                WonPanel(false, players[0].WonGameScore);

            }
            else if (maxScore == players[1].TotalScore)
            {
                players[1].WonGameScore++;
                WonPanel(true, players[1].WonGameScore);
            }
            ResetScore();

            for (int j = 0; j < 2; j++)
            {
                gameScore[j] = 0;
                currentScore[j] = 0;
            }

            //Debug.LogError("score 1 = " + gameScore[0] + " - - score 2 = " + gameScore[1]);
        }
        else if (turn > 1)
        {
            if (currentScore[0] >= currentScore[1])
            {
                RoundPanel(false);
            }
            else
            {
                RoundPanel(true);
            }
        }


    }

    void ResetScore()
    {
        foreach (var VARIABLE in players)
        {
            VARIABLE.ResetScore();
        }

    }

    void WonPanel(bool isWon, int value)
    {
        if (isWon)
        {
            if (value < 2)
            {
                // TUR KAZANMA PANELİ
                //Debug.LogError("TURU KAZANDIN");
                AudioManager.instance.ShortApllause();

                UIManager.Instance.loseTourImg.enabled = false;
                UIManager.Instance.loseTourTxt.enabled = false;
                UIManager.Instance.wonTourImg.enabled = true;
                UIManager.Instance.Tour();
                //GameScene.instance.EndTour(true, true);
            }
            else
            {
                endGame = true;
                // OYUN KAZANMA PANELİ
                //Debug.LogError("Oyunu Kazandın");
                AudioManager.instance.ShortApllause();

                UIManager.Instance.loseGameImg.enabled = false;
                UIManager.Instance.loseGameTxt.enabled = false;
                UIManager.Instance.wonGameImg.enabled = true;
                UIManager.Instance.Game();

                UpdateRank(true);

                //GameScene.instance.EndTour(false, true);
            }

        }
        else
        {
            if (value < 2)
            {
                // TUR KAYBETME PANELİ
                //Debug.LogError("TURU KAYBETTİN");
                AudioManager.instance.LoseRound();

                //UIManager.Instance.loseGameTxt.text = "Turu Kaybettin";
                UIManager.Instance.wonTourImg.enabled = false;
                UIManager.Instance.loseTourImg.enabled = true;
                UIManager.Instance.loseTourTxt.enabled = true;
                UIManager.Instance.Tour();
                //GameScene.instance.EndTour(true, false);
            }
            else
            {
                endGame = true;
                // OYUN KAYBETME PANELİ
                //Debug.LogError("Oyunu KAYBETTİN");
                AudioManager.instance.LoseRound();

                //UIManager.Instance.loseGameTxt.text = "Kaybettin";
                UIManager.Instance.wonGameImg.enabled = false;
                UIManager.Instance.loseGameImg.enabled = true;
                UIManager.Instance.loseGameTxt.enabled = true;
                UIManager.Instance.Game();

                UpdateRank(false);

                //GameScene.instance.EndTour(false, false);
            }
        }
    }



    void RoundPanel(bool isWon)
    {
        if (isWon)
        {
            // ROUND KAZANMA PANELİ
            //Debug.LogError("ROUND KAZANDIN");
            AudioManager.instance.ShortApllause();

            UIManager.Instance.roundTxt.text = turn + ". Deste";
            turn++;
            GameScene.instance.OpenRoundPanel();
        }
        else
        {
            // ROUND KAYBETME PANELİ
            //Debug.LogError("ROUND KAYBETTİN");
            AudioManager.instance.LoseRound();

            UIManager.Instance.roundTxt.text = turn + ". Deste";
            turn++;
            GameScene.instance.OpenRoundPanel();
        }

    }

    void FirstRound()
    {
        UIManager.Instance.roundTxt.text = turn + ". Deste";
        turn++;
        GameScene.instance.OpenRoundPanel();
    }

    public void EndTour()
    {
        foreach (var VARIABLE in players)
        {
            VARIABLE.ResetScore();
            VARIABLE.CurrentScore = 0;
        }

        if (!endGame)
        {
            StartGame();
        }
        else
        {
            GameScene.instance.Restart();
        }

    }

    void DisplayTable()
    {
        foreach (Card card in turnManager.table.cards)
        {
            if (card.isOpen)
                Debug.Log(card.value + " " + card.type);
        }
    }

    void Win(Player player)
    {
        //Debug.LogError(player.name + " oyunu kazandı");
        //StartGame();
    }

    void UpdateRank(bool isWon)
    {
        int currentRank = PlayerPrefs.GetInt("rank", 1);
        if (isWon)
            PlayerPrefs.SetInt("rank", currentRank + UnityEngine.Random.Range(1, 4));
        else
            PlayerPrefs.SetInt("rank", currentRank - UnityEngine.Random.Range(1, 4));


        int playerNum;

        if (!players[0].isBot)
            playerNum = 0;
        else
            playerNum = 1;

        players[playerNum].Rank = PlayerPrefs.GetInt("rank", 1);
    }

    private void Awake()
    {
        instance = this;
    }
}