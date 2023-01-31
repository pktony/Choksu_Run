namespace Define
{
    #region SCENE #############################################################
    public enum SceneIndex
    {
        Title,
        In_Game,
        Loading,
    }
    #endregion

    #region GAMEPLAY ##########################################################
    public enum ObstacleType
    {
        empty = 99999,
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