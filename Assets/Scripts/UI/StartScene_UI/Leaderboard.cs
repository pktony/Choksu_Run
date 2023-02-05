using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreObj;

    private ScrollRect scrollRect;

    private TextMeshProUGUI[] scores;

    private void Awake()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        scores = new TextMeshProUGUI[NetworkManager.MAX_RANK_COUNT];
    }

    private async void Start()
    {
        InitializeLeaderboard();
    }

    private async void InitializeLeaderboard()
    {
        Transform scoreObjParent = scrollRect.content;
        for (int i = 0; i < NetworkManager.MAX_RANK_COUNT; i++)
        {
            GameObject obj = Instantiate(scoreObj, scoreObjParent);
            obj.name = (i + 1).ToString();
            scores[i] = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        await GetLeaderboardDB();
    }

    public async Task GetLeaderboardDB()
    {
        var taskData = GameManager.Inst.Network.GetRankDB();

        var dics = await taskData;

        for (int i = 0; i < scores.Length; i++)
        {
            if (i < dics.Length)
            {
                scores[i].text = string.Format("Rank {0}  ID {1}  Score {2}",
                    i + 1, dics[i][NetworkManager.path_ID], dics[i][NetworkManager.path_Score]);
            }
            else
            {
                scores[i].text = string.Format("Rank {0}", i + 1);
            }
        }
    }
}
