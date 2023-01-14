using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Fade : MonoBehaviour
    {
        private Image image;
        private CanvasGroup group;

        [SerializeField]
        private float fadeTime = 1.0f;

        private void Awake()
        {
            image = GetComponent<Image>();
            group = GetComponent<CanvasGroup>();
        }

        public void ShowImage()
        {
            StartCoroutine(AdjustAlpha(true));
        }

        public void HideImage()
        {
            StartCoroutine(AdjustAlpha(false));
        }

        private IEnumerator AdjustAlpha(bool isShow)
        {
            float deltaTime = Time.deltaTime / fadeTime;
            if (isShow)
            {
                while (group.alpha < 1f)
                {
                    group.alpha += deltaTime;
                    yield return null;
                }
                group.alpha = 1f;
                group.interactable = true;
                group.blocksRaycasts = true;
            }
            else
            {
                while (group.alpha > 0f)
                {
                    group.alpha -= deltaTime;
                    yield return null;
                }
                group.alpha = 0f;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }
    }
}