
public class DavinciCommon : Common {
    public const int GAME_CODE = 1;
    public const string IDENTIFIER_START_DAVINCICODE = "start_davincicode";
    public const string IDENTIFIER_SELECT_FIELD_CARD = "select_field_card";
    public const string IDENTIFIER_SELECT_USER_CARD = "select_user_card";
    public const string IDENTIFIER_TURN = "turn";
    public const string IDENTIFIER_GAME_CARD_INFO = "game_card_info";
    public const string IDENTIFIER_OPEN_CARD = "open_card";
    public const string IDENTIFIER_GAME_FINISH = "game_finish";

    public enum PLAY_STATE
    {
        INIT = 0,
        WAITING = 1,
        SELECT_CARD = 2,
        GAME_OVER = 3
    }

    public static GamePlayInitData gamePlayingData = new GamePlayInitData();
}
