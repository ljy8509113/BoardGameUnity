
public class RequestSelectNumber : RequestBase {
    public int number;

    public RequestSelectNumber(int number) : base(Common.IDENTIFIER_SELECT_NUMBER)
    {
        this.number = number;
    }
}
