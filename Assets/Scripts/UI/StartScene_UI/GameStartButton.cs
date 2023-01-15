using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        //씬 로드
        Debug.Log("Game Start");
        SceneLoadManager.Inst.LoadScene_Ads(Define.SceneIndex.In_Game, Define.AdType.Interstitial);
    }
}
