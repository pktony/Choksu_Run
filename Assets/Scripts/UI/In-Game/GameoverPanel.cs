using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton = default;
    [SerializeField] private Button homeButton = default;

    [Header("Init Objects")]
    [SerializeField] ObstacleSpawner spawner;
    

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(ReturnToHome);
    }

    private void RestartGame()
    {
        GameManager.Inst.IsGameOver = false;

        GameManager.Inst.SetSpeed();

        GameManager.Inst.Status = GameManager.GameStatus.Stop;

        GameManager.Inst.sound.StopBGM();

        GameManager.Inst.Score.Initialize();

        spawner.InitRoutine();

        SceneLoadManager.Inst.LoadScene_NoAds(Define.SceneIndex.In_Game,
            () => GameManager.Inst.ResetPooledObjects());
    }

    private void ReturnToHome()
    {
        GameManager.Inst.Status = GameManager.GameStatus.Stop;

        GameManager.Inst.sound.StopBGM();

        SceneLoadManager.Inst.LoadScene_NoAds(Define.SceneIndex.Title,
            () => GameManager.Inst.ResetPooledObjects()) ;
    }
}
