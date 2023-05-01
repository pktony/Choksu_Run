using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameoverPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton = default;
    [SerializeField] private Button homeButton = default;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(ReturnToHome);
    }

    private void RestartGame()
    {
        SceneLoadManager.Inst.LoadScene_NoAds(Define.SceneIndex.In_Game,
            () => GameManager.Inst.ResetPooledObjects());
    }

    private void ReturnToHome()
    {
        SceneLoadManager.Inst.LoadScene_NoAds(Define.SceneIndex.Title,
            () => GameManager.Inst.ResetPooledObjects()) ;
    }
}
