using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float score;

    [SerializeField]
    private float scoreSpeed;
    
    public Action<float> onScoreChange;

    public float Score
    {
        get => score;
        set
        {
            score = value;
            onScoreChange?.Invoke(score) ;
        }
    }

    private void Start()
    {
        Score = 0;
    }

    private void Update()
    {
        if(!GameManager.Inst.IsGameOver)
            Score += scoreSpeed * Time.deltaTime;
    }
}
