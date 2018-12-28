public class RequestConnectionRoom : RequestBase {
    public string nickName; 

    public RequestConnectionRoom(int roomNo, string nickName) : base(Common.IDENTIFIER_CONNECT_ROOM, roomNo)
    {
        this.nickName = nickName;
    }
}
