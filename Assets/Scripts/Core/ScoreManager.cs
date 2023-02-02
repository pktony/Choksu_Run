using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float score;
    private int inGameCoin;

    private float scoreSpeed;
    private readonly float scoreMultiplier = 10f;
    
    public Action<float> onScoreChange;
    public Action<int> onCoinChange;

    public float Score
    {
        get => score;
        set
        {
            score = value;
            onScoreChange?.Invoke(score) ;
        }
    }

    public int InGameCoin
    {
        get => inGameCoin;
        set
        {
            inGameCoin = value;
             onCoinChange?.Invoke(inGameCoin);
        }

    }

    private void Start()
    {
        Score = 0;
        InGameCoin = 0;
    }

    private void Update()
    {
        if (GameManager.Inst.IsGameOver) return;
        scoreSpeed = GameManager.Inst.speed * scoreMultiplier;
        Score += scoreSpeed * Time.deltaTime;
    }
}
