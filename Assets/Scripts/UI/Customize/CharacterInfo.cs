using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterInfo : MonoBehaviour, IPointerClickHandler
{
    private bool isLocked = false;

    private Define.Characters character;

    private Image characterIcon;
    private GameObject lockCover;
    private TextMeshProUGUI priceText;
    private TextMeshProUGUI nameText;

    public void InitializeInfos(CharacterDatas data, bool isLocked)
    {
        if (data == null) return;

        this.character = data.character;
        this.isLocked = isLocked;

        characterIcon = transform.GetChild(0).GetComponent<Image>();
        lockCover = characterIcon.transform.GetChild(0).gameObject;
        priceText = lockCover.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        characterIcon.sprite = data.icon;
        nameText.text = data.characterName;

        if (!this.isLocked)
        {
            lockCover.SetActive(false);
        }
        else
        {
            priceText.text = data.icon.ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // enum만 넘겨주면 resourceManager에서 데이터를 가져오는 식으로
        GameManager.Inst.CharManager.SetCurrentCharacter_Customize(character);
    }
}
