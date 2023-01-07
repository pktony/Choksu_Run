using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour //장애물 객체의 행동
{
    public float speed = 0f;
    public Vector2 startPostion;


    private void Awake()
    {
        startPostion = transform.position;
    }

    private void OnDisable()
    {
        transform.position = startPostion;
    }


    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if(transform.position.x < -18.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
