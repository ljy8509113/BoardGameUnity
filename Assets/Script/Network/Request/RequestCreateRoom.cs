public class RequestCreateRoom : RequestBase {
    public string title;
    public string nickName;
    public string password;

    public RequestCreateRoom(string title, string nickName, string password, int gameNo) : base(Common.IDENTIFIER_CREATE_ROOM, gameNo, -1)
    {
        //base.(Common.IDENTIFIER_CREATE_ROOM, gameNo, -1);
        this.title = title;
        this.nickName = nickName;
        this.password = password;
    }
}
