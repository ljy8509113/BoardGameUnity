public class RequestRoomInfo : RequestBase {

    
	public RequestRoomInfo(int gameNo, int roomNo) : base(Common.IDENTIFIER_ROOM_INFO, gameNo, roomNo)
    {
        
    }
}
