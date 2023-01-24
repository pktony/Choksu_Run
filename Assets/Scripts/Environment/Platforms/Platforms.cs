using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platforms : MonoBehaviour
{
    protected PoolingManager poolManager;

    [SerializeField]
    protected float speed = 0f;
    [Header("뭉텡이면 체크")]
    [SerializeField]
    protected bool isGroupedChild = false;

    private Vector2 startPostion;
    protected float leftEnd;

    private void Awake()
    {
        startPostion = transform.position;
        CalculateGroupSize();
    }

    private void Start()
    {
        poolManager = GameManager.Inst.PoolManager;
        leftEnd = GameManager.Inst.CameraManager.GetLeftEnd();
    }

    private void OnDisable()
    {
        transform.position = startPostion;
    }

    private void Update()
    {
        if (isGroupedChild) return;
        MovePlatform();
    }

    /// <summary>
    /// 게임오브젝트가 만들어질 때 그룹 사이즈 계산
    /// </summary>
    protected virtual void CalculateGroupSize() { }

    protected abstract void MovePlatform();
    protected abstract bool TouchAction();
    protected abstract void ReturnPool();
}
