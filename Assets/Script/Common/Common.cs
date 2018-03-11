
public class Common {

    public enum IP_KINDS
    {
        KOITT = 0,
        HOME = 1
    }

    public const int GAME_NO = 1;

    public const string IDENTIFIER_GAME_ROOM_LIST = "game_room_list";
    public const string IDENTIFIER_GAMING_USER = "gaming_user";
    public const string IDENTIFIER_CONNECT_ROOM = "connection_room";
    public const string IDENTIFIER_CREATE_ROOM = "create_room";
    public const string IDENTIFIER_TEST = "test";
    public const string IDENTIFIER_LOGIN = "login";

    public const int LIST_COUNT = 10;

    public static string getUUID()
    {
        return "uuid_test";
    }

    public static IP_KINDS ipKinds = IP_KINDS.HOME;

    public static string getIp()
    {
        switch (ipKinds)
        {
            case IP_KINDS.KOITT :
                return "192.168.0.8";
            case IP_KINDS.HOME:
                return "211.201.206.24";

        }
        return "211.201.206.24";
    }

    public static string KEY_AUTO_LOGIN = "autoLogin";

    public static string JOIN_URL = getIp() + "/";

   
}
