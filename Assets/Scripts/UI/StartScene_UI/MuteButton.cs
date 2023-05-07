using System;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Button button = default;

    [SerializeField] private Image muteIcon = default;
    [SerializeField] private Sprite[] muteSprites = default;

    private bool isMute = false;

    private void Awake()
    {
        button.onClick.AddListener(ChangeMuteStatus);
    }

    private void ChangeMuteStatus()
    {
        isMute = !isMute;

        if (isMute)
        {
            GameManager.Inst.sound.MuteAll();
            muteIcon.sprite = muteSprites[0];
        }
        else
        {
            GameManager.Inst.sound.UnMuteAll();
            muteIcon.sprite = muteSprites[1];
        }
    }
}
