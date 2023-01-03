using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_StartScene : MonoBehaviour
{
    private Currency_UI currency;

    [SerializeField]
    private GameStartButton startButton;

    [SerializeField]
    private UIs.UI_Slide loadingPanel;

    private void Awake()
    {
        currency = transform.GetChild(0).GetComponent<Currency_UI>();
    }


    public void MoveLoadingPanel()
    {
        loadingPanel.Slide();
    }
}
