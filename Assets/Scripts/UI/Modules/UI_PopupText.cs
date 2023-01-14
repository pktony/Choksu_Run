using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UIs
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_PopupText : MonoBehaviour
    {
        private Image background;
        private TextMeshProUGUI popupText;
        private CanvasGroup group;
        private RectTransform rect;
        private WaitForSeconds fadeWaitSeconds;

        [SerializeField]
        private Sprite backgroundImg;

        [SerializeField]
        private float fontSize;

        [SerializeField]
        private float fadeWaitTime = 1.0f;
        [SerializeField]
        private float fadeTime = 1f;
        [SerializeField]
        private float moveSpeed = 0.1f;

        [SerializeField]
        [Header("0 ~ 1")]
        private Vector2 position;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            group = GetComponent<CanvasGroup>();
            group.interactable = false;
            group.blocksRaycasts = false;

            background = transform.GetChild(0).GetComponent<Image>();
            background.sprite = backgroundImg;
            if (backgroundImg == null)
                background.color = Color.clear;

            popupText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            fadeWaitSeconds = new WaitForSeconds(fadeWaitTime);
        }

        public void ShowText(string text, float fontSize, Color color)
        {
            group.alpha = 1.0f;
            rect.anchoredPosition = Vector2.zero;
            popupText.text = text;
            popupText.fontSize = fontSize;
            popupText.color = color;

            StartCoroutine(AdjustAlpha());
        }

        private IEnumerator AdjustAlpha()
        {
            yield return fadeWaitSeconds;
            float timer = 0f;
            float fadeSpeed = 1f / fadeTime * Time.unscaledDeltaTime;
            while (timer < fadeTime)
            {
                timer += Time.unscaledDeltaTime;
                group.alpha -= fadeSpeed;
                rect.anchoredPosition += Vector2.up * moveSpeed;
                yield return null;
            }
            group.alpha = 0f;
        }
    }
}