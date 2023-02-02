using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UIs
{
    public class UI_PopupText : MonoBehaviour
    {
        private TextMeshPro popupText;
        private WaitForSeconds fadeWaitSeconds;

        [SerializeField]
        private float moveSpeed = 0.1f;

        [SerializeField]
        private float fadeDuration = 1f;

        private void Awake()
        {
            popupText = GetComponent<TextMeshPro>();
        }

        private void OnEnable()
        {
            StartCoroutine(AdjustAlpha());
        }

        private void Update()
        {
            transform.position += Vector3.up * moveSpeed;
        }

        public void ShowText(string text, float fadeWaitTime, Vector2 position, Color color, float fontSize)
        {
            transform.position = position;
            popupText.text = text;
            popupText.color = color;
            popupText.fontSize = fontSize;
            fadeWaitSeconds = new WaitForSeconds(fadeWaitTime);
        }

        private IEnumerator AdjustAlpha()
        {
            yield return fadeWaitSeconds;
            float timer = 0f;
            float fadeSpeed = 1f / fadeDuration * Time.unscaledDeltaTime;
            while (timer < fadeDuration)
            {
                timer += Time.unscaledDeltaTime;
                Color newColor = popupText.color - new Color(0, 0, 0, fadeSpeed);
                popupText.color = newColor;
                yield return null;
            }
            popupText.color = Color.clear;
            GameManager.Inst.PoolManager.ReturnPopupText(this);
        }
    }
}