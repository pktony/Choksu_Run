using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<ObstacleType, Queue<GameObject>> obstaclePool = new();
    private Dictionary<CurrencyType, Queue<GameObject>> currencyPool = new();

    [SerializeField]
    private GameObject[] obstaclePrefabs;

    [SerializeField]
    private GameObject[] currencyPrefabs;

    [Header("Pooling Count")]
    [Tooltip("0 : obstcle  1 : currency")]
    [SerializeField]
    private int[] poolingCounts;


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
                // 어떤게 낫지 ?
                //ObstacleType otype = (ObstacleType)(object)type;
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

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(obstaclePool.Count < 1)
            InitializePool();
    }

    #region PRIVATE 함수 ########################################################
    private void InitializePool()
    {
        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            obstaclePool[(ObstacleType)i] = new Queue<GameObject>();
            for (int j = 0; j < poolingCounts[0]; j++)
            {
                obstaclePool[(ObstacleType)i].Enqueue(CreatePoolingObject((ObstacleType)i, i, this.transform));
            }
        }

        for(int i = 0; i < currencyPrefabs.Length; i++)
        {
            currencyPool[(CurrencyType)i] = new Queue<GameObject>();
            for (int j = 0; j < poolingCounts[1]; j++)
            {
                currencyPool[(CurrencyType)i].Enqueue(CreatePoolingObject((CurrencyType)i, i, this.transform));
            }
        }
    }

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
