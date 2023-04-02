using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentsSwitcher : MonoBehaviour
{
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private ScrollRect[] scrollContents;

    [SerializeField]
    private GameObject itemInfoObj;

    private void Awake()
    {
        InitializeButtonActions();
    }

    private void Start()
    {

        for (int i = 0; i < scrollContents.Length; i++)
        {
            var resources = GameManager.Inst.resource.GetItemInfos((Define.itemTypes)i);
            for (int j = 0; j < resources.Length; j++)
            {
                GameObject obj = Instantiate(itemInfoObj, scrollContents[i].content);
                obj.GetComponent<ItemInfo>().InitializeInfos(resources[j], "temp", 10, false);
            }
        }
    }

    private void InitializeButtonActions()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() =>
            {
                int clickIndex = button.transform.GetSiblingIndex();
                for (int j = 0; j < buttons.Length; j++)
                {
                    if (j == clickIndex)
                    {
                        scrollContents[j].gameObject.SetActive(true);

                        //grid 크기에 맞춰서 scrollRect의 content height 계산해서 적용시킬 것
                        continue;
                    }

                    scrollContents[j].gameObject.SetActive(false);
                }
            });
        }
    }
}
