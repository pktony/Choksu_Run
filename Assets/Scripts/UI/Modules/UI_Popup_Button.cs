using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class UI_Popup_Button : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UI_Popup popupWindow;

        public void OnPointerClick(PointerEventData eventData)
        {
            popupWindow.OpenCloseWindow();
        }
    }
}