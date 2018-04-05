
public class RequestReady : RequestBase {
    bool isReady;
    int roomNo;

    public RequestReady(bool isReady, int roomNo) : base(Common.IDENTIFIER_READY)
    {
        this.isReady = isReady;
        this.roomNo = roomNo;
    }
	
}
