using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Define;


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
        GameManager.Inst.sound.StopBGM();

        GameManager.Inst.Score.Initialize();

        GameManager.Inst.IsGameOver = false;

        GameManager.Inst.SetSpeed();

        SceneLoadManager.Inst.LoadScene_NoAds(SceneIndex.In_Game, null);
        //SceneLoadManager.Inst.LoadScene_Ads(SceneIndex.In_Game, AdType.Banner);
    }
}
