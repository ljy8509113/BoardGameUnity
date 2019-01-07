public class RequestRoomPassword : RequestBase {
	public string password;

	public RequestRoomPassword(int roomNo, string password):base(Common.IDENTIFIER_ROOM_PASSWORD, -1, roomNo){
		this.password = password;
	}
}
