namespace Define
{
    #region SCENE #############################################################
    public enum SceneBuildIndex : byte
    {
        Title = 0,
        InGame,
    }
    #endregion

    #region GAMEPLAY ##########################################################
    public enum ObstacleType
    {
        SingleJump = 0,
        DoubleJump,
        Slide,
        WarningLaser,
        FlyingObject,
    }

    public enum CurrencyType
    {
        bronze,
        group_DoubleJump,
    }

    public enum SceneIndex
    {
        Title,
        In_Game,
        Loading,
    }
    #endregion

    public enum AdType
    {
        Banner,
        Interstitial,
        Reward,
    }

    #region SOUND ##############################################################
    public enum SFX
    {
        Jump, Slide, GetCoin, GameOver
    }

    public enum BGM
    {
        LobbyScene, GameScene
    }
    #endregion
}