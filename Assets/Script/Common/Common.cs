﻿using System.Text.RegularExpressions;

public class Common {

    public enum GAME_KINDS
    {
        DAVINCICODE = 1
    }

    // public const int GAME_NO = 1;
    public const int NO_DATA = -1;

    //game before
    public const string IDENTIFIER_GANE_LIST 		= "game_list";
    public const string IDENTIFIER_GAME_ROOM_LIST   = "game_room_list";
    public const string IDENTIFIER_GAMING_USER      = "gaming_user";
    public const string IDENTIFIER_CONNECT_ROOM     = "connection_room";
    public const string IDENTIFIER_CREATE_ROOM      = "create_room";
    public const string IDENTIFIER_TEST             = "test";
    public const string IDENTIFIER_LOGIN            = "login";
    public const string IDENTIFIER_JOIN             = "join";
    public const string IDENTIFIER_ROOM_USERS 		= "room_users";
    public const string IDENTIFIER_OUT_ROOM         = "out_room";
	public const string IDENTIFIER_ROOM_PASSWORD	= "room_password";
    public const string IDENTIFIER_START            = "start";
    public const string IDENTIFIER_ROOM_INFO        = "room_info";
    public const string IDENTIFIER_INIT_GAME        = "init_game";
    public const string IDENTIFIER_READY            = "ready";
    

    public const int LIST_COUNT = 10;

    public static string getUUID()
    {
        return "uuid_test";
    }
    
    public static string KEY_AUTO_LOGIN = "autoLogin";
    public static string KEY_EMAIL = "email";
    public static string KEY_PASSWORD = "password";
    public static string KEY_NICKNAME = "nickname";

    public static bool isMailCheck(string email)
    {
        Regex mailValidator = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
        return mailValidator.IsMatch(email);
    }
    
    public enum USER_STATE
    {
        NONE = 0,
        GAME_WAITING = 1,
		READY = 2,
        PLAING = 3
    }

    public enum IP_KINDS
    {
        KOITT = 0,
        HOME = 1,
        LAMU = 2
    }

    public enum USER_TYPE
    {
        MASTER = 0,
        USER = 1,
        COMPUTER = 2
    }
}
