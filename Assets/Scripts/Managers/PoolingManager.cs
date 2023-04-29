using System.Collections.Generic;
using Define;
using UnityEngine;

public class PoolingManager : MonoBehaviour, IBootingComponent
{
    private Dictionary<ObstacleType, Queue<GameObject>> obstaclePool = new();
    private Dictionary<CurrencyType, Queue<GameObject>> currencyPool = new();

    private Queue<UIs.UI_PopupText> popupTexts = new();

    [SerializeField]
    private GameObject[] obstaclePrefabs;

    [SerializeField]
    private GameObject[] currencyPrefabs;

    [SerializeField]
    private GameObject[] uiPoolPrefabs;

    [Header("Pooling Count")]
    [Tooltip("0 : obstcle  1 : currency")]
    [SerializeField]
    private int[] poolingCounts;

#region IBootingComponent
    private bool isReady = false;
    public bool IsReady => isReady;
#endregion

    /// <summary>
    /// Pool 초기화
    /// </summary>
    public void InitializePool()
    {
        if (obstaclePool.Count > 1 || currencyPool.Count > 1)
            return;

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            obstaclePool[(ObstacleType)i] = new Queue<GameObject>();
            for (int j = 0; j < poolingCounts[0]; j++)
            {
                obstaclePool[(ObstacleType)i].Enqueue(CreatePoolingObject((ObstacleType)i, i, this.transform));
            }
        }

        for (int i = 0; i < currencyPrefabs.Length; i++)
        {
            currencyPool[(CurrencyType)i] = new Queue<GameObject>();
            for (int j = 0; j < poolingCounts[1]; j++)
            {
                currencyPool[(CurrencyType)i].Enqueue(CreatePoolingObject((CurrencyType)i, i, this.transform));
            }
        }

        for (int i = 0; i < poolingCounts[2]; i++)
        {
            GameObject uiObj = CreatePoolingObject(UIPoolType.popupText, 0, this.transform);
            popupTexts.Enqueue(uiObj.GetComponent<UIs.UI_PopupText>());
        }

        isReady = true;
    }


    /// <summary>
    /// 오브젝트 풀 가져오기
    /// </summary>
    /// <typeparam name="T">0 : Obstacle,  1 : Currency</typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetPooledObject<T>(T type) where T : System.Enum
    {
        GameObject obj = null;

        switch (type)
        {
            case ObstacleType:
                // implicit type change
                // ObstacleType otype = (ObstacleType)(object)type;
                // reflection
                ObstacleType oType = (ObstacleType)System.Enum.Parse(type.GetType(), type.ToString());
                if (obstaclePool[oType].Count > 0)
                {
                    obj = obstaclePool[oType].Dequeue();
                }
                else
                {
                    obstaclePool[oType].Enqueue(CreatePoolingObject(oType, (int)oType, this.transform));
                    return GetPooledObject(oType);
                }
                break;
            case CurrencyType:
                CurrencyType ctype = (CurrencyType)(object)type;
                if (currencyPool[ctype].Count > 0)
                {
                    obj = currencyPool[ctype].Dequeue();
                }
                else
                {
                    currencyPool[ctype].Enqueue(CreatePoolingObject(ctype, (int)ctype, this.transform));
                    return GetPooledObject(ctype);
                }
                break;
            default:
                Debug.LogError("INVALID POOLING TYPE");
                break;
        }

        return obj;
    }


    public UIs.UI_PopupText GetPooledUIs(UIPoolType type)
    {
        UIs.UI_PopupText popupText;
        if (popupTexts.Count > 0)
        {
            popupText = popupTexts.Dequeue();
        }
        else
        {
            GameObject uiObj = CreatePoolingObject(type, (int)type, this.transform);
            return uiObj.GetComponent<UIs.UI_PopupText>();
        }
        return popupText;
    }
    public void ReturnPooledObject<T>(GameObject uselessObj, T type) where T:System.Enum
    {
        switch(type)
        {
            case ObstacleType:
                obstaclePool[(ObstacleType)(object)type].Enqueue(uselessObj);
                break;
            case CurrencyType:
                currencyPool[(CurrencyType)(object)type].Enqueue(uselessObj);
                break;
        }
        
        uselessObj.SetActive(false);
    }

    //임시
    // 풀링할 게 더 생기면 수정 필요
    public void ReturnPopupText(UIs.UI_PopupText popupText)
    {
        popupTexts.Enqueue(popupText);
        popupText.gameObject.SetActive(false);
    }

    #region PRIVATE 함수 ########################################################
    private GameObject CreatePoolingObject<T>(T type, int index, Transform parent)
    {
        GameObject obj;
        switch (type)
        {
            case ObstacleType:
                obj = Instantiate(obstaclePrefabs[index], parent);
                break;
            case CurrencyType:
                obj = Instantiate(currencyPrefabs[index], parent);
                break;
            case UIPoolType:
                obj = Instantiate(uiPoolPrefabs[index], parent);
                break;
            default:
                obj = null;
                Debug.LogError("NO VALID PREFAB FOUND. LOOK INSPECTOR.");
                break;
        }
        
        obj.SetActive(false);
        return obj;
    }
    #endregion
}
