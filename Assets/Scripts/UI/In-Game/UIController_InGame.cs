using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController_InGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    public CanvasGroup gameoverGroup;

    private void Start()
    {
        GameManager.Inst.Score.onScoreChange += UpateScore;
        GameManager.Inst.Score.onCoinChange += UpdateCoin;
    }

    public void ShowGameoverUIs()
    {
        gameoverGroup.alpha = 1.0f;
    }

    public void HideGameoverUIs()
    {
        gameoverGroup.alpha = 0f;
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
