using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public GameObject prefab = null;
    private List<GameObject> grounds = new List<GameObject>();
    private GameObject ground;

    //[SerializeField]
    private float speed;

    private float leftEnd;
    private float xSize;

    private Coroutine routine = null;


    private void Awake()
    {
        xSize = prefab.GetComponent<BoxCollider2D>().size.x;
        leftEnd = -Camera.main.ScreenToWorldPoint(Screen.width * Vector3.right).x; 
        for (int i = 0; i < 20; i++)
        {
            GameObject gr = Instantiate(prefab, transform);
            grounds.Add(gr);
            gr.transform.position = new Vector3(leftEnd + xSize * i, -5.5f, 0f);
        }
    }

    private void Start()
    {
        InitRoutine();
    }

    public void InitRoutine()
    {
        ground = grounds[0];

        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(ScrollGrounds());
    }


    //public void InitGrounds() //게임씬을 벗어날때 
    //{
    //    foreach(GameObject ground in grounds)
    //    {
    //        Destroy(ground.gameObject);
    //    }

    //    grounds.Clear();
    //}


    private IEnumerator ScrollGrounds()
    {
        yield return new WaitUntil(() => GameManager.Inst.Status.Equals(GameManager.GameStatus.Run));

        while (GameManager.Inst.Status.Equals(GameManager.GameStatus.Run))
        {
            for (int i = 0; i < grounds.Count; i++)
            {
                if (leftEnd - xSize >= grounds[i].transform.position.x)
                {
                    for (int j = 0; j < grounds.Count; j++)
                    {
                        if (ground.transform.position.x < grounds[j].transform.position.x)
                            ground = grounds[j];
                    }
                    grounds[i].transform.position = new Vector2(ground.transform.position.x + xSize, -5.5f);
                }
            }

            for (int i = 0; i < grounds.Count; i++)
            {
                speed = GameManager.Inst.GetSpeed();
                grounds[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
            }

            yield return null;
        }

        yield return null;
    }

    //private void Update()
    //{
    //    if (GameManager.Inst.Status == GameManager.GameStatus.Run)
    //    {
    //        for (int i = 0; i < grounds.Count; i++)
    //        {
    //            speed = GameManager.Inst.speed;
    //            grounds[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
    //        }

    //        for (int i = 0; i < grounds.Count; i++)
    //        {
    //            if (leftEnd - xSize >= grounds[i].transform.position.x)
    //            {
    //                for (int j = 0; j < grounds.Count; j++)
    //                {
    //                    if (ground.transform.position.x < grounds[j].transform.position.x)
    //                        ground = grounds[j];
    //                }
    //                grounds[i].transform.position = new Vector2(ground.transform.position.x + xSize, -5.5f);
    //            }
    //        }
    //    }
    //}
}
