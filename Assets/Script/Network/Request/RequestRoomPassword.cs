public class RequestRoomPassword : RequestBase {
	public int roomNo;
	public string password;

	public RequestRoomPassword(int roomNo, string password):base(Common.IDENTIFIER_ROOM_PASSWORD){
		this.password = password;
		this.roomNo = roomNo;
	}
}
