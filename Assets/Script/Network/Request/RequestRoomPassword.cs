public class RequestRoomPassword : RequestBase {
	public string password;

	public RequestRoomPassword(int gameNo, int roomNo, string password):base(Common.IDENTIFIER_ROOM_PASSWORD, gameNo, roomNo){
		this.password = password;
	}
}
