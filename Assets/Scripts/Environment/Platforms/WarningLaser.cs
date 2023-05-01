using System.Collections;

using UnityEngine;
using Define;   

/// <summary>
/// 작업중
/// 워닝 레이저 크기 설정 필요
/// </summary>
public class WarningLaser : Platforms<ObstacleType>
{
    private Animator anim;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private int spawnNum = 3;
    [SerializeField]
    private float warningDuration = 2.0f;
    [SerializeField]
    private float spawnDelay = 0.5f;

    private bool isWarning = false;
    private float pos_Top;
    private float pos_Bot;

    public bool IsWarning
    {
        get => isWarning;
        set
        {
            isWarning = value;
            if(isWarning)
            {
                StartCoroutine(WanringOff_Delay());
            }
            else
            {
                StartCoroutine(SpawnFlyingObjects());
            }
            anim.SetBool("isWarning", isWarning);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        poolManager = GameManager.Inst.PoolManager;

        //TODO : 매니저 준비완료(초기화) 대기
        float rightEnd = GameManager.Inst.CameraManager.GetRightEnd();
        spriteRenderer.transform.localScale = new Vector2(rightEnd * 2f, 1f);
        spriteRenderer.transform.localPosition = new Vector2(-rightEnd * 0.5f, 0f);
    }

    private void OnEnable()
    {
        IsWarning = true;

    }

    public void SetRandomHeight()
    {
        
    }

    private IEnumerator WanringOff_Delay()
    {
        yield return new WaitForSeconds(warningDuration);
        IsWarning = false;
    }

    private IEnumerator SpawnFlyingObjects()
    {
        int spawnCount = 0;

        while (spawnCount < spawnNum)
        {
            var obj = GameManager.Inst.PoolManager.GetObstacle(ObstacleType.FlyingObject);
            obj.gameObject.SetActive(true);
            obj.transform.position = transform.position;
            spawnCount++;
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitUntil(() => !isWarning);
        ReturnPool();
    }

    protected override void MovePlatform() { }

    protected override bool TouchAction() { return false; }

    protected override void EnablingAction() { }

    protected override void ReturnPool()
    {
        poolManager.ReturnObstacle(this);
    }
}
