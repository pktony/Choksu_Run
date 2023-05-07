using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;


public class ObstacleSpawner : MonoBehaviour
{
    PoolingManager poolManager;
    private LevelDesign levelDesign;

    private char[] levels;   // Copy of Level Design levelString
    private int cursor = 0;

    /// <summary>
    /// 장애물 종류
    /// </summary>
    private List<char> obstacleChars;

    private const float DEFAULT_OBSTACLE_TIME = 1.5f;
    private const float DEFAULT_COIN_TIME = 0.2f;

    [SerializeField]
    private Transform spawnPoint;

    [Header("Spawn Position Limit")]
    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;

    private Coroutine routine = null;


    private void Awake()
    {
        levelDesign = GetComponent<LevelDesign>();
    }

    private void Start()
    {
        poolManager = GameManager.Inst.PoolManager;

        InitRoutine();
    }

    public void InitRoutine()
    {
        cursor = 0;

        levels = levelDesign.GetLevels(out obstacleChars).ToArray();

        if (routine != null)
        {
            StopCoroutine(routine);
        }
        routine = StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        yield return new WaitUntil(() => GameManager.Inst.Status == GameManager.GameStatus.Run);

        float time;
        while (cursor < levels.Length)
        {
            if (GameManager.Inst.IsGameOver) yield break;

            if (obstacleChars.Contains(levels[cursor]))
            {
                if (levels[cursor] - '0' < 10)
                {// 장애물
                    var obj = poolManager.GetObstacle((ObstacleType)levels[cursor] - '0');
                    time = levelDesign.GetTime((ObstacleType)levels[cursor] - '0');
                    obj.transform.position = new Vector2(
                spawnPoint.transform.position.x, obj.transform.position.y);
                    obj.gameObject.SetActive(true);
                }
                else
                {// 코인
                    var obj = poolManager.GetCurrency((CurrencyType)(levels[cursor] - 'a'));
                    //obj.transform.position += maxHeight * Vector3.up;
                    time = levelDesign.GetTime((CurrencyType)(levels[cursor] - 'a'));
                    obj.transform.position = new Vector2(
                spawnPoint.transform.position.x, obj.transform.position.y);
                    obj.gameObject.SetActive(true);
                }
            }
            else
            {// 예외 처리
                var obj = poolManager.GetObstacle(ObstacleType.SingleJump);
                time = DEFAULT_OBSTACLE_TIME;
                obj.transform.position = new Vector2(
                spawnPoint.transform.position.x, obj.transform.position.y);
                obj.gameObject.SetActive(true);
            }

            cursor++;
            if (levelDesign.isObstacleTest)
                cursor %= levels.Length;

            //TODO : waitseconds cache
            yield return new WaitForSeconds(time);
        }
    }
}
