using System.Collections.Generic;
using Define;
using UnityEngine;

using UIs;

public class PoolingManager : MonoBehaviour, IBootingComponent
{
    private Dictionary<ObstacleType, Queue<Platforms<ObstacleType>>> obstaclePool = new();
    private Dictionary<CurrencyType, Queue<Platforms<CurrencyType>>> currencyPool = new();

    private Dictionary<ObstacleType, List<Platforms<ObstacleType>>> activeObstacles = new();
    private Dictionary<CurrencyType, List<Platforms<CurrencyType>>> activeCurrencies = new();

    private Queue<UI_PopupText> popupTexts = new();
    private List<UI_PopupText> activePopupTexts = new();

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

        for(int i = 0; i < obstaclePrefabs.Length; i++)
        {
            var type = (ObstacleType)i;
            obstaclePool[type] = new();
            activeObstacles[type] = new();
            CreateObstacles(type, transform);
        }
        for (int i = 0; i < currencyPrefabs.Length; i++)
        {
            var type = (CurrencyType)i;
            currencyPool[type] = new();
            activeCurrencies[type] = new();
            CreateCurrencies(type, transform);
        }
        CreatePopupUIs(UIPoolType.popupText, transform);

        isReady = true;
    }

    public Platforms<ObstacleType> GetObstacle(ObstacleType type)
    {
        Platforms<ObstacleType> obj = null;
        if (obstaclePool[type].Count > 0)
        {
            obj = obstaclePool[type].Dequeue();
            activeObstacles[type].Add(obj);
        }
        else
        {
            CreateObstacles(type, this.transform);
            return GetObstacle(type);
        }

        return obj;
    }

    public Platforms<CurrencyType> GetCurrency(CurrencyType type)
    {
        Platforms<CurrencyType> obj = null;
        if (currencyPool[type].Count > 0)
        {
            obj = currencyPool[type].Dequeue();
            activeCurrencies[type].Add(obj);
        }
        else
        {
            CreateCurrencies(type, transform);
            return GetCurrency(type);
        }

        return obj;
    }

    public UI_PopupText GetPooledUIs(UIPoolType type)
    {
        UI_PopupText popupText;
        if (popupTexts.Count > 0)
        {
            popupText = popupTexts.Dequeue();
            activePopupTexts.Add(popupText);
        }
        else
        {
            CreatePopupUIs(type, this.transform);
            return GetPooledUIs(type);
        }
        return popupText;
    }

    public void ReturnObstacle(Platforms<ObstacleType> uselessObj)
    {
        obstaclePool[uselessObj.Type].Enqueue(uselessObj);
        activeObstacles[uselessObj.Type].Remove(uselessObj);

        uselessObj.gameObject.SetActive(false);
    }

    public void ReturnCurrency(Platforms<CurrencyType> uselessObj)
    {
        currencyPool[uselessObj.Type].Enqueue(uselessObj);
        activeCurrencies[uselessObj.Type].Remove(uselessObj);

        uselessObj.gameObject.SetActive(false);
    }

    //임시
    // 풀링할 게 더 생기면 수정 필요
    public void ReturnPopupText(UI_PopupText popupText)
    {
        popupTexts.Enqueue(popupText);
        activePopupTexts.Remove(popupText);
        popupText.gameObject.SetActive(false);
    }

    public void ReturnAllActivePools()
    {
        for (int i = 0; i < activeObstacles.Count; i++)
        {
            if (activeObstacles[(ObstacleType)i].Count <= 0) continue;

            while (activeObstacles[(ObstacleType)i].Count > 0)
                ReturnObstacle(activeObstacles[(ObstacleType)i][0]);

            Debug.Log($"All {(ObstacleType)i} Returned");
        }
        for (int i = 0; i < activeCurrencies.Count; i++)
        {
            if (activeCurrencies[(CurrencyType)i].Count <= 0) continue;

            while(activeCurrencies[(CurrencyType)i].Count > 0)
                ReturnCurrency(activeCurrencies[(CurrencyType)i][0]);

            Debug.Log($"All {(CurrencyType)i} Returned");
        }
    }

    #region PRIVATE 함수 ########################################################
    private void CreateObstacles(ObstacleType type, Transform parent)
    {
        while (obstaclePool[type].Count < poolingCounts[0])
        {
            Platforms<ObstacleType> obstacle =
                Instantiate(obstaclePrefabs[(int)type], parent).GetComponent<Platforms<ObstacleType>>();
            obstacle.gameObject.SetActive(false);
            obstaclePool[type].Enqueue(obstacle);
        }
    }

    private void CreateCurrencies(CurrencyType type, Transform parent)
    {
        while (currencyPool[type].Count < poolingCounts[1])
        {
            Platforms<CurrencyType> currency =
                Instantiate(currencyPrefabs[(int)type], parent).GetComponent<Platforms<CurrencyType>>();
            currency.gameObject.SetActive(false);
            currencyPool[type].Enqueue(currency);
        }
    }

    private void CreatePopupUIs(UIPoolType type, Transform parent)
    {
        while (popupTexts.Count < poolingCounts[2])
        {
            UI_PopupText popupUI =
                Instantiate(uiPoolPrefabs[(int)type], parent).GetComponent<UI_PopupText>();
            popupTexts.Enqueue(popupUI);
            popupUI.gameObject.SetActive(false);
        }
    }
    #endregion
}
