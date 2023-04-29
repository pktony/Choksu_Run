using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class ResourceManager : MonoBehaviour, IBootingComponent
{
    private const string ITEM_PATH = "Items/";

    private Dictionary<itemTypes, Sprite[]> itemResources;

#region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
#endregion

    private void Awake()
    {
        InitializeResources_Items();

        isReady = true;
    }

    private void InitializeResources_Items()
    {
        itemResources = new Dictionary<itemTypes, Sprite[]>();

        var folder = new DirectoryInfo(Application.dataPath + "/Resources/" + ITEM_PATH);
        var dirs = folder.GetDirectories();
        for(int i = 0; i < dirs.Length; i++)
        {
            var _dirs = dirs[i].ToString().Split('/');
            if (_dirs == null) return;
            var lastDir = _dirs[_dirs.Length - 1];
            Sprite[] loadedSprite = Resources.LoadAll<Sprite>(ITEM_PATH + lastDir);
            if (loadedSprite != null)
            {
                itemResources[(itemTypes)i] = loadedSprite;
            }
        }
    }

    public Sprite[] GetItemInfos(itemTypes type)
    {
        if (itemResources[type] == null)
        {
            Debug.LogWarning($"resource of {type} does not exist");
            return null;
        }

        return itemResources[type];
    }

    #region old version
    //임시
    // 나중에 로딩 후 입력할 수 있도록 변경
    [SerializeField]
    private CharacterDatas[] characterData;


    public CharacterDatas[] CharacterData => characterData;

    #endregion
}
