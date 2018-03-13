public class RequestConnectionRoom : RequestBase {

    public int roomId;
    public string nickName; 

    public RequestConnectionRoom(int roomId, string nickName) : base(Common.IDENTIFIER_CONNECT_ROOM)
    {
        this.roomId = roomId;
        this.nickName = nickName;
    }
}
