

public class RequestGameCardInfo : RequestBase {

    public RequestGameCardInfo(int gameNo, int roomNo) : base(DavinciCommon.IDENTIFIER_GAME_CARD_INFO, gameNo, roomNo)
    {
        
    }
}
