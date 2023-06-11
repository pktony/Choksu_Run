using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Define;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton = default;
    [SerializeField] private Button homeButton = default;

    [SerializeField] private CanvasGroup canvasGroup = default;

    [Header("Init Objects")]
    [SerializeField] ObstacleSpawner spawner;


    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(ReturnToHome);
    }

    public void SetActiveGameoverUI(bool isActive)
    {
        canvasGroup.alpha = isActive ? 1.0f : 0.0f;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }

    private void RestartGame()
    {
        GameManager.Inst.IsGameOver = false;

        GameManager.Inst.SetSpeed();

        GameManager.Inst.Status = GameManager.GameStatus.Stop;

        GameManager.Inst.sound.StopBGM();

        GameManager.Inst.Score.Initialize();

        spawner.InitRoutine();

        SceneLoadManager.Inst.LoadScene_Ads(SceneIndex.In_Game, AdType.Interstitial,
            () => GameManager.Inst.ResetPooledObjects());
    }

    private void ReturnToHome()
    {
        GameManager.Inst.Status = GameManager.GameStatus.Stop;

        GameManager.Inst.sound.StopBGM();

        SceneLoadManager.Inst.LoadScene_Ads(SceneIndex.Title, AdType.Interstitial,
            () => GameManager.Inst.ResetPooledObjects());

        //SceneLoadManager.Inst.LoadScene_NoAds(Define.SceneIndex.Title,
        //    () => GameManager.Inst.ResetPooledObjects());
    }
}
