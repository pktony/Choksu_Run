using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작업중
/// 워닝 레이저 크기 설정 필요
/// </summary>
public class WarningLaser : MonoBehaviour
{
    private Animator anim;
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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        IsWarning = true;
    }

    private void Start()
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


        while(spawnCount < spawnNum)
        {
            GameObject obj = GameManager.Inst.PoolManager.GetPooledObject(Define.ObstacleType.FlyingObject);
            obj.SetActive(true);
            spawnCount++;
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitUntil(() => !isWarning);
        gameObject.SetActive(false);
    }

}
