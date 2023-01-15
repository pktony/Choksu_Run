namespace Define
{
    #region GAMEPLAY ##########################################################
    public enum ObstacleType
    {
        SingleJump = 0,
        DoubleJump,
        Slide,
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
        Jump, Slide
    }

    public enum BGM
    {
        MainScene
    }
    #endregion
}