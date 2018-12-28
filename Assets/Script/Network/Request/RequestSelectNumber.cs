
public class RequestSelectNumber : RequestBase {
    public int index;
    public RequestSelectNumber(int gameNo, int roomNo, int index) : base(Common.IDENTIFIER_SELECT_NUMBER, gameNo, roomNo)
    {
        this.index = index;
    }
}
