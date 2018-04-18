public class RequestInitGame : RequestBase{
    public int roomNo;
    public RequestInitGame(int roomNo) : base(Common.IDENTIFIER_INIT_GAME)
    {
        this.roomNo = roomNo;
    }
    
}
