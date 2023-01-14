using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platforms : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0f;
    private Vector2 startPostion;

    [SerializeField]
    protected Define.ObstacleType type;

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
        MovePlatform();
    }

    protected abstract void MovePlatform();
    protected abstract bool TouchAction();
    protected abstract void ReturnPool();
}
