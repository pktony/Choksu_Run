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

    [Header("Flags")]
    private bool isGameOver = false;

    private PlayerDatas playerDatas;
    private int gold;

    #region DELEGATE ##########################################################
    public Action<int, int> onGoldChange; // Current Gold, Goal Gold
    #endregion

    #region PROPERTY ##########################################################
    public ScoreManager Score => score;
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
            GameOver();
        }
    }
    #endregion

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        score = GetComponent<ScoreManager>();
        saves = GetComponent<SaveManager>();   
    }

    private void Start()
    {
        Gold = saves.LoadDatas(out playerDatas) ? playerDatas.gold : 0;
    }

    private void GameOver()
    {

    }
}
