using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Platforms<T> : MonoBehaviour
{
    protected PoolingManager poolManager;
    protected Rigidbody2D rigid;

    protected float speed = 0f;
    [Header("뭉텡이면 체크")]
    [SerializeField]
    protected bool isGroupedChild = false;

    [SerializeField]
    protected T type;

    private Vector2 startPostion;
    protected float leftEnd;

    protected virtual void Awake()
    {
        InitializeRigidbody();
        startPostion = transform.position;
        CalculateGroupSize();
    }

    protected virtual void InitializeRigidbody()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
    }

    private void Start()
    {
        poolManager = GameManager.Inst.PoolManager;
        leftEnd = GameManager.Inst.CameraManager.GetLeftEnd();
    }

    private void OnEnable()
    {
        EnablingAction();
    }

    private void OnDisable()
    {
        transform.position = startPostion;
    }

    private void FixedUpdate()
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
    /// <summary>
    /// 오브젝트 풀에서 가져왔을 때 할 실행되는 함수
    /// OnEnable에서 실행
    /// </summary>
    protected abstract void EnablingAction();
    protected abstract void ReturnPool();
}
