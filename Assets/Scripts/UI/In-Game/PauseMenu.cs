using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton = default;
    [SerializeField] private Button resumeButton = default;
    [SerializeField] private Button HomeButton = default;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        HomeButton.onClick.AddListener(ReturnToHome);
    }

    private void RestartGame()
    {
        throw new NotImplementedException();
    }

    private void ResumeGame()
    {
        GameManager.Inst.IsPause = false;
    }

    private void ReturnToHome()
    {
        throw new NotImplementedException();
    }
}
