using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

/// <summary>
/// TODO : Make Popup Queue
/// </summary>
public class CommonPopup : Singleton<CommonPopup>, IBootingComponent
{
    [SerializeField] private GameObject popupObject = default;

    [SerializeField] private TextMeshProUGUI titleText = default;
    [SerializeField] private TextMeshProUGUI contentText = default;
    [SerializeField] private Button confirmButton = default;

    [SerializeField] private Animation anim = default;

    [SerializeField] private string[] animationClipNames;

    #region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
    #endregion

    private enum PopupAnimationType : byte { open = 0, close = 1}

    protected override void Awake()
    {
        base.Awake();

        isReady = true;
    }

    public void OpenCommonPopup(string title, string content, Action completeAction = null)
    {
        confirmButton.onClick.AddListener(() =>
        {
            GameManager.Inst.sound.PlaySFX(Define.SFX.Click);
            anim.Play(animationClipNames[(int)PopupAnimationType.close]);
            completeAction?.Invoke();
        });

        titleText.text = title;
        contentText.text = content;
        popupObject.SetActive(true);
        anim.Play(animationClipNames[(int)PopupAnimationType.open]);
    }


    public void OnAnimationEnd()
    {
        popupObject.SetActive(false);
    }
}
