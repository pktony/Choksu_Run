using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;

public class ScrollSizeController : MonoBehaviour
{
    public GameObject characterInfoWindowObj;
    private RectTransform rect;

    private int camWidth;
    private readonly int space = 100;

    private void Awake()
    {
        camWidth = Camera.main.pixelWidth;
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(camWidth + (int)Characters.characterCount * space, rect.sizeDelta.y);
    }

    private void Start()
    {
        InitializeCharacterInfos();
    }

    public void InitializeCharacterInfos()
    {
        CharacterDatas[] datas = GameManager.Inst.resource.CharacterData;
    
        for (int i = 0; i < (int)Characters.characterCount; i++)
        {
            GameObject obj = Instantiate(characterInfoWindowObj, this.transform);
            if (obj.TryGetComponent<CharacterInfo>(out CharacterInfo info))
            {
                // 나중에 플레이어가 해당 캐릭터를 언락했는지에 대한 정보를 입력
                info.InitializeInfos(datas[i], false);  
            }
            else
            {
                info.InitializeInfos(null, true);
            }
        }
    }
}
