using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class UI_Slide_Button : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UI_Slide slider;

        private void Awake()
        {
            //slider = GetComponentInParent<UI_Slide>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            slider.Slide();
        }
    }
}
