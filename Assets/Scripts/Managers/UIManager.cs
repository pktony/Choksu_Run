using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //임시
    UIController_StartScene startScene;

    private void Awake()
    {
        startScene = FindObjectOfType<UIController_StartScene>();
    }

    public void OnLoadStart()
    {
        startScene.MoveLoadingPanel();
    }

    public void OnLoadComplete()
    {
        startScene.MoveLoadingPanel();
    }
}
