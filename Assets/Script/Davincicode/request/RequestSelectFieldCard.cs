
public class RequestSelectFieldCard : RequestBase {
    public int index;

    public RequestSelectFieldCard(string email, int index, int roomNo) : base(DavinciCommon.IDENTIFIER_SELECT_FIELD_CARD, DavinciCommon.GAME_CODE, roomNo, email)
    {
        this.index = index;
    }
}
