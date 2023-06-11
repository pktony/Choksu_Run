using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController_InGame : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    [SerializeField] private GameoverPanel gameoverPanel;


    private void Start()
    {
        GameManager.Inst.Score.onScoreChange += UpateScore;
        GameManager.Inst.Score.onCoinChange += UpdateCoin;
    }

    public void ShowGameoverUIs()
    {
        gameoverPanel.SetActiveGameoverUI(true);
    }

    public void HideGameoverUIs()
    {
        gameoverPanel.SetActiveGameoverUI(false);
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
