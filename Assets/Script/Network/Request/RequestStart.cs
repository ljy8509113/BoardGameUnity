
public class RequestStart : RequestBase {
    public int roomNo;
    
    public RequestStart(int roomNo) : base(Common.IDENTIFIER_GAME_START)
    {
        this.roomNo = roomNo;        
    }
}
