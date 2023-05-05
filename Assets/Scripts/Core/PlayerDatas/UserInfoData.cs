
public class UserInfoData
{
    private static string uid;


    public static string GetUID() => uid;
    public static void SetUID(string playerUID)
    {
        uid = playerUID;
    }
}
