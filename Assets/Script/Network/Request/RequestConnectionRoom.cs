public class RequestConnectionRoom : RequestBase {
    public string nickName;
    public bool isComputer;

    public RequestConnectionRoom(int roomNo, string nickName, bool isComputer) : base(Common.IDENTIFIER_CONNECT_ROOM, roomNo)
    {
        this.isComputer = isComputer;
        this.nickName = nickName;
    }
}
