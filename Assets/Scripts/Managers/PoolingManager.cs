using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

public class PoolingManager : Singleton<PoolingManager>
{
    private Dictionary<ObstacleType, Queue<GameObject>> obstaclePool = new();

    [SerializeField]
    private GameObject[] obstaclePrefabs;

    private int poolingCount = 2;

    public GameObject GetPooledObject(ObstacleType type)
    {
        GameObject obj = null;
        if (obstaclePool[type].Count > 0)
        {
            obj = obstaclePool[type].Dequeue();
        }
        else
        {
            obstaclePool[type].Enqueue(CreateObstacle((int)type, this.transform));
            GetPooledObject(type);
        }
        return obj;
    }

    public void ReturnPooledObject(GameObject uselessObj, ObstacleType type)
    {
        obstaclePool[type].Enqueue(uselessObj);
        uselessObj.SetActive(false);
    }

    protected override void Initialize()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            obstaclePool[(ObstacleType)i] = new Queue<GameObject>();
            for (int j = 0; j < poolingCount; j++)
            {
                obstaclePool[(ObstacleType)i].Enqueue(CreateObstacle(i, this.transform));
            }
        }
    }

    private GameObject CreateObstacle(int index, Transform parent)
    {
        GameObject obstacle = Instantiate(obstaclePrefabs[index], parent);
        obstacle.SetActive(false);
        return obstacle;
    }
}
