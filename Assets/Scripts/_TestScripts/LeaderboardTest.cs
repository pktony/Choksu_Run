using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardTest : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField scoreInput;
    public UnityEngine.UI.Button button;
    public Leaderboard board;


    private void Awake()
    {
        button.onClick.AddListener(UpdateBoard);
    }

    private async void UpdateBoard()
    {
        GameManager.Inst.sound.PlaySFX(Define.SFX.Click);

        if (scoreInput.text.Length == 0 || idInput.text.Length == 0) return;


        int score = int.Parse(scoreInput.text);
        GameManager.Inst.Network.UpdateScoreDB(idInput.text, score);
        await board.GetLeaderboardDB();
    }
}
