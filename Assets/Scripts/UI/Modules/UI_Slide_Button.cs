using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    [RequireComponent(typeof(Button))]
    public class UI_Slide_Button : MonoBehaviour
    {
        [SerializeField] private UI_Slide slider;

        private Button slideButton = default;

        private Action openActionListener;
        private Action closeActionListener;

        private IEnumerator closeActionHandler;

        private void Awake()
        {
            slideButton = GetComponent<Button>();
            slideButton.onClick.AddListener(Slide);
        }

        public void SetActivateListeners(Action openingAction, Action closeAction)
        {
            openActionListener = openingAction;
            closeActionListener = closeAction;
        }

        private void Slide()
        {
            GameManager.Inst.sound.PlaySFX(Define.SFX.Click);

            bool isOpen = slider.Slide();

            if (isOpen)
            {
                openActionListener?.Invoke();
            }
            else
            {
                if (closeActionHandler != null)
                    StartCoroutine(closeActionHandler = CloseActionCoroutine());
            }

        }

        private IEnumerator CloseActionCoroutine()
        {
            yield return new WaitUntil(() => !slider.IsPanelSliding);

            closeActionListener?.Invoke();
            closeActionHandler = null;
        }
    }
}
