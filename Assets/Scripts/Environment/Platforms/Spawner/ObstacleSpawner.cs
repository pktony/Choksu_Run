using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class ObstacleSpawner : MonoBehaviour
{
    PoolingManager poolManager;
    private LevelDesign levelDesign;

    private char[] levels;   // Copy of Level Design levelString
    private int cursor = 0;

    /// <summary>
    /// 장애물 종류
    /// </summary>
    private List<char> obstacleChars;

    private const float DEFAULT_OBSTACLE_TIME = 1.5f;
    private const float DEFAULT_COIN_TIME = 0.2f;

    [Header("Spawn Position Limit")]
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;

    private void Awake()
    {
        levelDesign = GetComponent<LevelDesign>();
        levels = levelDesign.GetLevels(out obstacleChars).ToArray();
    }

    private void Start()
    {
        poolManager = GameManager.Inst.PoolManager;
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        float time;
        GameObject obj = null;
        while (cursor < levels.Length)
        {
            if (obstacleChars.Contains(levels[cursor]))
            {
                if (levels[cursor] - '0' < 10)
                {// 장애물
                    obj = poolManager.GetPooledObject((ObstacleType)levels[cursor] - '0');
                    time = levelDesign.GetTime((ObstacleType)levels[cursor] - '0');
                }
                else
                {// 코인
                    obj = poolManager.GetPooledObject((CurrencyType)(levels[cursor] - 'a'));
                    //obj.transform.position += maxHeight * Vector3.up;
                    time = levelDesign.GetTime((CurrencyType)(levels[cursor] - 'a'));
                }
            }
            else
            {// 예외 처리
                obj = poolManager.GetPooledObject(ObstacleType.SingleJump);
                time = DEFAULT_OBSTACLE_TIME;
            }
            obj.SetActive(true);

            cursor++;
            if (levelDesign.isObstacleTest)
                cursor %= levels.Length;
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
