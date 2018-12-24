
public class RequestSelectNumber : RequestBase {
    public int index;
    public RequestSelectNumber(int index) : base(Common.IDENTIFIER_SELECT_NUMBER)
    {
        this.index = index;
    }
}
