using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UIs;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private RankElementPool rankElementPool = default;
    [SerializeField] private UI_Slide_Button slideButton = default;

    [SerializeField] private GameObject loadingCircle = default;
    [SerializeField] private VerticalLayoutGroup layoutGroup = default;

    private ScrollRect scrollRect;

    private void Awake()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();

        slideButton.SetActivateListeners(() => OpenAction(), () => CloseAction());
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Inst.Network.IsReady && rankElementPool.IsPoolReady);
        InitializeLeaderboard();
    }

    private async void InitializeLeaderboard()
    {
        Transform scoreObjParent = scrollRect.content;
        for (int i = 0; i < NetworkManager.MAX_RANK_COUNT; i++)
        {
            var element = rankElementPool.GetRankElement(scoreObjParent);
            element.gameObject.name = (i + 1).ToString();
        }

        float totalHeight = (rankElementPool.ActiveElementPool[0].Height + layoutGroup.spacing)
            * rankElementPool.ActiveElementPool.Count;
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, totalHeight);

        loadingCircle.SetActive(true);

        await GetLeaderboardDB();

        loadingCircle.SetActive(false);
    }

    public async Task GetLeaderboardDB()
    {
        var taskData = GameManager.Inst.Network.GetRankDB();

        var dics = await taskData;

        for (int i = 0; i < rankElementPool.ActiveElementPool.Count; i++)
        {
            if (i < dics.Count)
            {
                rankElementPool.ActiveElementPool[i].SetRank(i + 1, dics[i][NetworkManager.path_ID].ToString(),
                    dics[i][NetworkManager.path_Score].ToString());
            }
            else
            {
                rankElementPool.ActiveElementPool[i].SetRank(i + 1, null, null);
            }

            rankElementPool.ActiveElementPool[i].gameObject.SetActive(true);
        }

        scrollRect.verticalNormalizedPosition = 1f;
    }


    private void CloseAction()
    {
        rankElementPool.ReleaseAllElements();
    }

    private void OpenAction()
    {
        rankElementPool.ReleaseAllElements();
        InitializeLeaderboard();
    }
}
