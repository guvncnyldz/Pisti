﻿using System;
using System.Collections;
using TMPro;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public delegate void PushHandler(Card card);

    public bool isBot;

    public string name;

    private int rank;
    private int currentCurrentScore;
    private int totalScore;
    private int wonGameScore;    // Kaç oyun kazanıldığı

    public int Rank
    {
        get { return rank; }
        set
        {
            rank = value;

            if (rankTxt != null)
                rankTxt.text = value.ToString();
        }
    }

    public int CurrentScore
    {
        get { return currentCurrentScore; }
        set
        {
            currentCurrentScore = value;
            currentScoreTxt.text = value.ToString();
        }
    }

    public int TotalScore
    {
        get { return totalScore; }

        set
        {
            totalScore = value;
            totalScoreTxt.text = value.ToString();
        }
    }

    public int WonGameScore
    {
        get { return wonGameScore; }

        set
        {
            wonGameScore = value;
            wonGameScoreTxt.text = value.ToString();
        }
    }

    public Hand hand;


    public int pistiCount;
    public int scoreCardCount;

    public Transform cardPlace;

    public TextMeshProUGUI nameTxt, totalScoreTxt, currentScoreTxt, wonGameScoreTxt, rankTxt;

    private void Awake()
    {
        hand = new Hand();

        //if (nameTxt != null)
        if (isBot)
        {
            name = PlayerPrefs.GetString("name", "opponent");
            nameTxt.text = name;
        }
        //nameTxt.text = name[UnityEngine.Random.Range(0, name.Length)];

        CurrentScore = 0;
        pistiCount = 0;
        scoreCardCount = 0;
        TotalScore = 0;

        int randomRank = UnityEngine.Random.Range(1, 4) * ((UnityEngine.Random.Range(0,1) * 2) - 1);

        if (isBot)
        {
            Rank = PlayerPrefs.GetInt("rank", 1);
            Rank += randomRank;

            if(Rank < 1)
                Rank = 1;
        }
    }

    public void CalculateTotalScore()
    {
        TotalScore += CurrentScore;

        CurrentScore = 0;
    }

    public void ResetScore()
    {
    }


    public abstract void PushCard(Table table, PushHandler callBack, Player enemy);
}