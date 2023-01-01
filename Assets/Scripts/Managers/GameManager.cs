using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Managers")]
    public UIManager ui;
    public SoundManager sound;
    public ResourceManager resource;


    [Header("Flags")]
    private bool isGameOver = false;

    private ScoreManager score;

    #region PROPERTY ##########################################################
    public ScoreManager Score => score;

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

    protected override void Initialize()
    {
        score = GetComponent<ScoreManager>();
    }

    private void GameOver()
    {

    }
}
