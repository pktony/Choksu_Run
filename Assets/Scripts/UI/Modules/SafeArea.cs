using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform rectTransform = default;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            SetSafeArea();
        }


        private void SetSafeArea()
        {
            var safeAreaRect = Screen.safeArea;

            Vector2 newAnchorMin = safeAreaRect.position;
            Vector2 newAnchorMax = safeAreaRect.position + safeAreaRect.size;

            newAnchorMin.x /= Screen.width;
            newAnchorMin.y /= Screen.height;

            newAnchorMax.x /= Screen.width;
            newAnchorMax.y /= Screen.height;

            rectTransform.anchorMin = newAnchorMin;
            rectTransform.anchorMax = newAnchorMax;
        }
    }
}