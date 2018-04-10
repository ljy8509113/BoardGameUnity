
public class RequestSelectNumber : RequestBase {
    int number;

    public RequestSelectNumber(int number) : base(Common.IDENTIFIER_SELECT_NUMBER)
    {
        this.number = number;
    }
}
