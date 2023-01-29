using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class UIManager : MonoBehaviour
{
    //임시
    UIController_StartScene titleController;
    UIController_InGame inGameController;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        switch(arg0.buildIndex)
        {
            case (int)SceneBuildIndex.Title:
                titleController = FindObjectOfType<UIController_StartScene>();
                break;

            case (int)SceneBuildIndex.InGame:
                inGameController = FindObjectOfType<UIController_InGame>();
                break;
            default:
                titleController = FindObjectOfType<UIController_StartScene>();
                inGameController = FindObjectOfType<UIController_InGame>();
                break;
        }
    }

    public void OnLoadStart()
    {
        titleController.MoveLoadingPanel();
    }

    public void OnLoadComplete()
    {
        titleController.MoveLoadingPanel();
    }

    public void ShowGameoverUI()
    {
        inGameController.ShowGameoverUIs();
    }
}
