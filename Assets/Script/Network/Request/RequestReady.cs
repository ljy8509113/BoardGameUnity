
public class RequestReady : RequestBase {
    public bool isReady;
    public int roomNo;

    public RequestReady(bool isReady, int roomNo) : base(Common.IDENTIFIER_READY)
    {
        this.isReady = isReady;
        this.roomNo = roomNo;
    }
	
}
