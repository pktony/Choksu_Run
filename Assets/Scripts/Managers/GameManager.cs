using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;

using Define;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    public enum GameStatus { Stop, Run }

    public GameStatus Status { get; set; } //Is InGame or Not


    [Header("Managers")]
    //public UIManager ui;
    public SoundManager sound;
    public ResourceManager resource;
    public UIManager uiManager;
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private SaveManager saves;
    [SerializeField] private ScoreManager score;
    [SerializeField] private PoolingManager poolManager;
    [SerializeField] private CamManager camManager;
    [SerializeField] private NetworkManager networkManager;

    private bool isGameOver = false;
    private bool isPause = false;

    private PlayerDatas playerDatas;
    private int gold;

    private IEnumerator bootWaitHandler;

    [Header("게임 속도")]
    public float speed; //Inject on Inspector , Default : speed 5 , gravity 3
    public float gravityScale;

    [Header("광고 관련")]
    [SerializeField] AdType gameStartAd;
    [Range(0f, 1f)]
    [SerializeField] float adExposureProbability;

    #region DELEGATE ##########################################################
    public Action<int, int> onGoldChange; // Current Gold, Goal Gold
    public Action onPause;
    public Action onResume;
    #endregion

    #region PROPERTY ##########################################################
    public ScoreManager Score => score;
    public PoolingManager PoolManager => poolManager;
    public CamManager CameraManager => camManager;
    public CharacterManager CharManager => characterManager;
    public NetworkManager Network => networkManager;
    public int Gold
    {
        get => gold;
        set
        {
            onGoldChange?.Invoke(gold, value);
            gold = value;
        }
    }

    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            isGameOver = value;
            if (isGameOver)
                GameOver();
        }
    }

    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;

            if(isPause)
            {
                Time.timeScale = 0.0f;
                SetStatus(GameStatus.Stop);
                onPause?.Invoke();
            }
            else
            {
                Time.timeScale = 1.0f;
                onResume?.Invoke();
            }
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        poolManager.InitializePool();
        sound.Initialize();

        InitializeApplicationSettings();

        if (bootWaitHandler == null)
            StartCoroutine(bootWaitHandler = ChecKBootingComponents());
    }

    private void InitializeApplicationSettings()
    {
        Application.targetFrameRate = 60;
    }

    private IEnumerator ChecKBootingComponents()
    {
        float startTime = Time.time;

        //TODO : find a method to reduce garbage
        var bootingComponents = FindObjectsOfType<MonoBehaviour>().OfType<IBootingComponent>();

        foreach (var component in bootingComponents)
        {
            float eachTime = 0f;
            while (!component.IsReady)
            {
                eachTime += Time.deltaTime;
                if (eachTime > 5f) goto NotInitialized;
                yield return null;
            }

            Debug.Log($"{component} initialized");
            continue;

        NotInitialized:
            Debug.LogError($"{component} not initialized");
            //CommonPopup.Inst.OpenCommonPopup("Error", "Failed to Initialize Game, Quitting",
            //    () => Application.Quit());
        }

        Debug.Log($"Booting Components Initialize Complete. : {Time.time - startTime} ms");

        SceneLoadManager.Inst.LoadScene_NoAds(SceneIndex.Title);
    }

    private void Start()
    {
        Gold = saves.LoadDatas(out playerDatas) ? playerDatas.gold : 0;
        SetStatus(GameStatus.Stop);
    }

    private void GameOver()
    {
        SetStatus(GameStatus.Stop);
        uiManager.ShowGameoverUI();
        InitSpeed();

        // TODO : 리더보드 갱신하기
        //networkManager.UpdateScoreDB(id, score.Score);
        // 점수 저장
        // 정렬
        // 순위 산출
    }

    public void SetStatus(GameStatus _status) => Status = _status;

    public void ResetPooledObjects()
    {
        poolManager.ReturnAllActivePools();
    }

    #region Time & Gravity
    public void InitSpeed() => speed = 0f;

    public void SetSpeed(float _speed = 5.0f) => speed = _speed;

    public float GetSpeed() => speed;
    #endregion
}
