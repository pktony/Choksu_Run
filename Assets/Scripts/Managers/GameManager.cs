using System;   
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Sub-Managers ######################################################
    [Header("Managers")]
    //public UIManager ui;
    public SoundManager sound;
    public ResourceManager resource;
    public UIManager uiManager;
    private SaveManager saves;
    public ScoreManager Score { get; private set; }
    public PoolingManager PoolManager { get; private set; }
    public CamManager CameraManager { get; private set; }
    public CharacterManager CharManager { get; private set; }
    public NetworkManager Network { get; private set; }
    #endregion

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
        Score = GetComponent<ScoreManager>();
        saves = GetComponent<SaveManager>();
        PoolManager = GetComponent<PoolingManager>();
        CameraManager = GetComponent<CamManager>();
        PoolManager.InitializePool();

        CharManager = GetComponent<CharacterManager>();
        sound.Initialize();
        Network = GetComponent<NetworkManager>();
    }

    private void Start()
    {
        Gold = saves.LoadDatas(out playerDatas) ? playerDatas.gold : 0;
    }

    private void GameOver()
    {
        uiManager.ShowGameoverUI();
        this.speed = 0f;

        // 점수 저장
        // 정렬
        // 순위 산출
    }
}
