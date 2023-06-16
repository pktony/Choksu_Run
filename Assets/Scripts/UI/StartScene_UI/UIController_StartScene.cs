using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Define;

public class UIController_StartScene : MonoBehaviour
{
    private Currency_UI currency;

    [SerializeField]
    private GameStartButton startButton;


    private void Awake()
    {
        currency = transform.GetChild(0).GetComponent<Currency_UI>();

        GameManager.Inst.CharManager.GetCurrentCharacter();

        AdManager.Inst.ShowAd(AdType.Banner, null);
    }
}
