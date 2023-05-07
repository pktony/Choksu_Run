using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIs
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Popup : MonoBehaviour
    {
        [Header("Routine Objects")]
        [SerializeField] Background _backGround;
        [SerializeField] GroundScroller _groundScroller;
        [SerializeField] CountUI _countUI;

        private CanvasGroup group;
        private RectTransform rect;

        enum animationStyle { shrink, pop, fade, instant }

        [SerializeField]
        private animationStyle style;

        [SerializeField]
        [Range(0.01f, 0.5f)]
        private float popupTime = 0.5f;

        private const float ERROR_CORRECTION_NUM = 0.05f;
        private bool isShow = true;
        private bool isMoving = false;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
            rect = GetComponent<RectTransform>();
        }

        public void OpenCloseWindow()
        {
            if (isMoving) return;
            switch (style)
            {
                case animationStyle.shrink:
                    StartCoroutine(AdjustWindowSize_Shrink(this.isShow));
                    break;
                case animationStyle.pop:
                    StartCoroutine(AdjustWindowSize_Pop(this.isShow));
                    break;
                case animationStyle.fade:
                    break;
                case animationStyle.instant:
                    break;
            }
            this.isShow = !this.isShow;
        }

        private IEnumerator AdjustWindowSize_Shrink(bool isShow)
        {
            isMoving = true;
            float deltaTime = Time.unscaledDeltaTime / popupTime;
            if (isShow)
            {// 나타나기
                ShowPanels();
                while (rect.localScale.x < 1f - ERROR_CORRECTION_NUM)
                {
                    rect.localScale = Vector2.Lerp(rect.localScale, Vector2.one, deltaTime);
                    yield return null;
                }
            }
            else
            {// 숨기기
                while (rect.localScale.x > ERROR_CORRECTION_NUM)
                {
                    rect.localScale = Vector2.Lerp(rect.localScale, Vector2.zero, deltaTime);
                    yield return null;
                }
                HidePanels();
            }
            isMoving = false;
        }

        private IEnumerator AdjustWindowSize_Pop(bool isShow)
        {
            float timer = 0f;
            float deltaTime = Time.unscaledDeltaTime / popupTime;
            if (isShow) ShowPanels();
            else
            {
                HidePanels();
                yield return null;
            }

            while (timer < popupTime)
            {
                timer += Time.unscaledDeltaTime;
                rect.localScale =
                    Vector2.Lerp(rect.localScale, Vector2.one * 1.1f, deltaTime);
                yield return null;
            }
            rect.localScale = Vector2.one;
        }

        private void ShowPanels()
        {
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        private void HidePanels()
        {
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;

            if(GameManager.Inst.Status.Equals(GameManager.GameStatus.Stop))
            {
                _countUI.gameObject.SetActive(true);
                _countUI.InitRoutine();
                _groundScroller.InitRoutine();
                _backGround.InitRoutine();
            }
        }
    }
}