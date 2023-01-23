using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platforms : MonoBehaviour
{
    protected PoolingManager poolManager;

    [SerializeField]
    protected float speed = 0f;
    private Vector2 startPostion;

    private void Awake()
    {
        startPostion = transform.position;
    }

    private void Start()
    {
        poolManager = PoolingManager.Inst;
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
