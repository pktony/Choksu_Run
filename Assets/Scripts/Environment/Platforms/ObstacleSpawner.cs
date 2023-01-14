using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class ObstacleSpawner : MonoBehaviour
{
    PoolingManager poolManager;
    private LevelDesign levelDesign;

    private int[] levels;   // Copy of Level Design levelString
    private int cursor = 0;

    private const float DEFAULT_OBSTACLE_TIME = 1.5f;

    private void Awake()
    {
        levelDesign = GetComponent<LevelDesign>();
        levels = levelDesign.GetLevels().ToArray();
    }

    private void Start()
    {
        poolManager = PoolingManager.Inst;
        StartCoroutine(CreateObstacle());
    }

    private IEnumerator CreateObstacle()
    {
        while (cursor < levels.Length)
        {
            float time = 0f;
            GameObject obj = poolManager.GetPooledObject((ObstacleType)levels[cursor]);

            time = levelDesign.GetTime((ObstacleType)levels[cursor]);
            obj.SetActive(true);

            cursor++;
            yield return new WaitForSeconds(time);
        }
    }

    //private int DeactiveObstacle() //겹침방지
    //{
    //    List<int> num = new List<int>();

    //    for (int i = 0; i < obstaclePool.Count; i++)
    //    {
    //        if (!obstaclePool[i].activeInHierarchy)
    //        {
    //            num.Add(i);
    //        }
    //    }
    //    int order = 0;
    //    if(num.Count > 0)
    //    {
    //        order = num[Random.Range(0, num.Count)];
    //    }

    //    return order;
    //}
}
