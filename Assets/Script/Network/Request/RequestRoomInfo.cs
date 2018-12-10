public class RequestRoomInfo : RequestBase {

    public int roomNo;
	public RequestRoomInfo(int roomNo) : base(Common.IDENTIFIER_ROOM_INFO)
    {
        this.roomNo = roomNo;
    }
}
