using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        GameManager.Inst.Score.onScoreChange += UpateScore;
    }

    private void UpateScore(float score)
    {
        scoreText.text = score.ToString("0000000");
    }
}
