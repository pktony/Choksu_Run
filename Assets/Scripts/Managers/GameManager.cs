using System;   
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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
            if(isGameOver)
                GameOver();
        }
    }

    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            Time.timeScale = isPause ? 0.0f : 1.0f;
            if (isPause)
                onPause?.Invoke();
            else
                onResume?.Invoke();
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
    }

    protected override void Awake()
    {
        base.Awake();
        sound.Initialize();
    }


    private void Start()
    {
        Gold = saves.LoadDatas(out playerDatas) ? playerDatas.gold : 0;
    }

    private void GameOver()
    {
        uiManager.ShowGameoverUI();
        //this.speed = 0f;
    }
}
