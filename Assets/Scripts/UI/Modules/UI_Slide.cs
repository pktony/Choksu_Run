using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIs
{
    public enum slideDirection : byte { horizontal = 0, vertical }
    public enum openingDiretion : int { positiveDir = 1, negativeDir = -1}

    /// <summary>
    /// 버튼만 보이도록 이미지를 이동한 상태에서 실행
    /// UI_Slide_Button을 자식 첫번쨰 오브젝트로 넣고 실행
    /// </summary>
    public class UI_Slide : MonoBehaviour
    {
        private RectTransform rect;

        private Vector2 panelSize;
        private Vector2 originalPos;
        private bool isOpen = false;
        private bool isMoving = false;

        private const float ERROR_CORRECTION_NUM = 0.2f;

        [SerializeField] private slideDirection slideDirection;
        [SerializeField] private openingDiretion openingDiretion;
        [SerializeField] private float slidingSpeed = 10f;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            panelSize = rect.sizeDelta;
        }

        public void Slide()
        {
            if(!isMoving)
                StartCoroutine(SlidePanels(openingDiretion));
        }

        private IEnumerator SlidePanels(openingDiretion dir)
        {
            isMoving = true;
            Vector2 newPos = rect.anchoredPosition;
            float boundary = panelSize.x;

            Vector2 destination = slideDirection == slideDirection.horizontal ?
                newPos + (int)dir * panelSize.x * Vector2.right :
                newPos + (int)dir * panelSize.y * Vector2.up;

            if (isOpen) destination = originalPos;
            else originalPos = rect.anchoredPosition;

            float timer = 0f;
            //시간 = 거리 / 속력
            float requiredTime = boundary / slidingSpeed * Time.unscaledDeltaTime;
            while(timer < requiredTime + ERROR_CORRECTION_NUM)
            {
                newPos = Vector2.Lerp(newPos, destination,
                    slidingSpeed * Time.unscaledDeltaTime);
                rect.anchoredPosition = newPos;

                timer += Time.unscaledDeltaTime;
                yield return null;
            }
            rect.anchoredPosition = destination;

            isOpen = !isOpen;
            isMoving = false;
        }
    }
}