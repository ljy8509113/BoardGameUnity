
public class RequestReady : RequestBase {
    public bool isReady;
    
    public RequestReady(bool isReady, int gameNo, int roomNo) : base(Common.IDENTIFIER_READY, gameNo, roomNo)
    {
        this.isReady = isReady;
        
    }
	
}
