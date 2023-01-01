using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ScoreManager score;

    private bool isGameOver = false;

    public ScoreManager Score => score;
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            isGameOver = value;
            Debug.Log(isGameOver);
        }
    }

    protected override void Initialize()
    {
        score = GetComponent<ScoreManager>();
    }
}
