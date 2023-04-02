using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour
{
    private bool isLocked = false;

    private Image itemIcon;
    private GameObject lockCover;
    private TextMeshProUGUI priceText;
    private TextMeshProUGUI nameText;

    public void InitializeInfos(Sprite icon, string itemName, int price, bool isLocked)
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        lockCover = itemIcon.transform.GetChild(0).gameObject;
        priceText = lockCover.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        itemIcon.sprite = icon;
        itemIcon.preserveAspect = true;
        nameText.text = itemName;

        if (!isLocked)
        {
            lockCover.SetActive(false);
        }
        else
        {
            priceText.text = price.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
