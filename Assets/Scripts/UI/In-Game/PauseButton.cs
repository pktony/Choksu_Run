using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class PauseButton : UI_Popup_Button
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            if(!GameManager.Inst.IsGameOver)
            {
                GameManager.Inst.sound.PlaySFX(Define.SFX.Click);
                GameManager.Inst.IsPause = !GameManager.Inst.IsPause;
                base.OnPointerClick(eventData);
            }
        }
    }
}
