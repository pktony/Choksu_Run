using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class UIController_InGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    private void Start()
    {
        GameManager.Inst.Score.onScoreChange += UpateScore;
        GameManager.Inst.Score.onCoinChange += UpdateCoin;
    }

    private void UpateScore(float score)
    {
        scoreText.text = score.ToString("0000000");
    }

    private void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
