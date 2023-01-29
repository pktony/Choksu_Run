using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class UI_Popup_Button : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UI_Popup popupWindow;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            popupWindow.OpenCloseWindow();
        }
    }
}