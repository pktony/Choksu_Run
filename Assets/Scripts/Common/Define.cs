namespace Define
{
    #region SCENE #############################################################
    public enum SceneIndex
    {
        Title,
        In_Game,
        Loading,
        Customize,
    }
    #endregion


    public enum Characters
    {
        kenny_Blue,
        Test1,
        Test2,
        Test3,
        characterCount,
    }

    #region GAMEPLAY ##########################################################
    public enum ObstacleType
    {
        empty = 99999,
        SingleJump = 0,
        DoubleJump,
        Slide,
        WarningLaser,
        FlyingObject,
        FallingObstacle,
        FallingObstacle_Fall,
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
    public enum PlayType { Direct, FadeIn }

    public enum SFX
    {
        Click, Complete, Dead, Jump, Slide, GetCoin, GameOver
    }

    public enum BGM
    {
        LobbyScene, GameScene
    }
    #endregion
}