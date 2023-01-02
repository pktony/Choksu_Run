using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private Button button;

    [SerializeField]
    private UIs.UI_Slide slidePanel;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        //씬 로드
        Debug.Log("Game Start");
        slidePanel.Slide();
    }
}
