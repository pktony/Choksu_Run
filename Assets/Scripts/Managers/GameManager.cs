using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameStatus { Stop, Run }

    public GameStatus Status { get; set; } //Is InGame or Not

    [Header("Managers")]
    //public UIManager ui;
    public SoundManager sound;
    public ResourceManager resource;
    public UIManager uiManager;
    private SaveManager saves;
    private ScoreManager score;
    private PoolingManager poolManager;
    private CamManager camManager;
    private CharacterManager characterManager;
    private NetworkManager networkManager;

    private bool isGameOver = false;
    private bool isPause = false;

    private PlayerDatas playerDatas;
    private int gold;


    [Header("게임 속도")]
    public float speed = 3.0f;
    public float gravityScale = 10f;

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
                SetStatus(GameStatus.Stop);
                Time.timeScale = 0.0f;
                onPause?.Invoke();
            }
            else
            {
                SetStatus(GameStatus.Run);
                Time.timeScale = 1.0f;
                onResume?.Invoke();
            }
        }
    }
    #endregion


    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        score = GetComponent<ScoreManager>();
        saves = GetComponent<SaveManager>();
        poolManager = GetComponent<PoolingManager>();
        camManager = GetComponent<CamManager>();
        poolManager.InitializePool();

        characterManager = GetComponent<CharacterManager>();
        sound.Initialize();
        networkManager = GetComponent<NetworkManager>();
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
        this.speed = 0f;

        // 점수 저장
        // 정렬
        // 순위 산출
    }

    public void SetStatus(GameStatus _status) => Status = _status;
}
