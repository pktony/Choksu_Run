using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class UI_Popup_Exit : MonoBehaviour, IPointerClickHandler
    {
        private UI_Popup popupWindow;

        private void Awake()
        {
            popupWindow = GetComponentInParent<UI_Popup>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            popupWindow.OpenCloseWindow();
        }
    }
}