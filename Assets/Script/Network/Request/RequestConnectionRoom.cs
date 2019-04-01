public class RequestConnectionRoom : RequestBase {
    public string nickName;
    public int userType;

    public RequestConnectionRoom(int roomNo, string nickName, int userType) : base(Common.IDENTIFIER_CONNECT_ROOM, roomNo)
    {
        this.userType = userType;
        this.nickName = nickName;
    }
}
