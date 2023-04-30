using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Define;

public class UIManager : MonoBehaviour, IBootingComponent
{
    //임시
    UIController_StartScene titleController;
    UIController_InGame inGameController;

#region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
#endregion
    
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoad;

        isReady = true;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        switch(arg0.buildIndex)
        {
            case (int)SceneIndex.Title:
                titleController = FindObjectOfType<UIController_StartScene>();
                break;

            case (int)SceneIndex.In_Game:
                inGameController = FindObjectOfType<UIController_InGame>();
                break;
            default:
                titleController = FindObjectOfType<UIController_StartScene>();
                inGameController = FindObjectOfType<UIController_InGame>();
                break;
        }
    }

    public void ShowGameoverUI()
    {
        inGameController.ShowGameoverUIs();
    }
}
