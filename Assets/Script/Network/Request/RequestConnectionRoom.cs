public class RequestConnectionRoom : RequestBase {

    public int roomNo;
    public string nickName; 

    public RequestConnectionRoom(int roomNo, string nickName) : base(Common.IDENTIFIER_CONNECT_ROOM)
    {
        this.roomNo = roomNo;
        this.nickName = nickName;
    }
}
