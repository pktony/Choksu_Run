using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public List<GameObject> obstaclePool = new List<GameObject>();
    public GameObject[] obstacles;
    public int count = 1;


    private void Awake()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            for (int j = 0; j < count; j++)
            {
                obstaclePool.Add(SetObstacle(obstacles[i], transform));
            }
        }
    }

    private void Start()
    {
        StartCoroutine(CreateObstacle());
    }


    private GameObject SetObstacle(GameObject obj, Transform parent)
    {
        GameObject obstacle = Instantiate(obj, parent);
        obstacle.SetActive(false);
        return obstacle;
    }

    private IEnumerator CreateObstacle()
    {
        while (true)
        {
            obstaclePool[DeactiveObstacle()].SetActive(true);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    private int DeactiveObstacle() //겹침방지
    {
        List<int> num = new List<int>();

        for (int i = 0; i < obstaclePool.Count; i++)
        {
            if (!obstaclePool[i].activeInHierarchy)
            {
                num.Add(i);
            }
        }
        int order = 0;
        if(num.Count > 0)
        {
            order = num[Random.Range(0, num.Count)];
        }

        return order;
    }
}
