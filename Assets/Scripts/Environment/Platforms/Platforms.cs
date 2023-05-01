using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Platforms<T> : MonoBehaviour where T : System.Enum
{
    protected GameManager gameManager;
    protected PoolingManager poolManager;
    protected Rigidbody2D rigid;
    private Collider2D platformCollider;

    protected float speed = 0f;
    [Header("뭉텡이면 체크")]
    [SerializeField]
    protected bool isGroupedChild = false;

    [SerializeField]
    protected T type;

    private Vector2 startPostion;
    protected float leftEnd;
    protected float size_X;

    public T Type => type;

    protected virtual void Awake()
    {
        InitializeRigidbody();
        startPostion = transform.position;
        GetSize();
        CalculateGroupSize();
    }

    protected virtual void InitializeRigidbody()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
    }

    private void Start()
    {
        gameManager = GameManager.Inst;
        poolManager = gameManager.PoolManager;
        leftEnd = gameManager.CameraManager.GetLeftEnd();
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

        if (GameManager.Inst.Status == GameManager.GameStatus.Run)
        {
            MovePlatform();
        }
    }

    private void GetSize()
    {
        if (!TryGetComponent<Collider2D>(out platformCollider)) return;

        size_X = platformCollider.bounds.size.x;
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
