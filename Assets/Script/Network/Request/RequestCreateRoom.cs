public class RequestCreateRoom : RequestBase {

    public int maxUser;
    public string title;
    public string nickName;
    public string password;

    public RequestCreateRoom(int maxUser, string title, string nickName, string password) : base(Common.IDENTIFIER_CREATE_ROOM)
    {
        this.maxUser = maxUser;
        this.title = title;
        this.nickName = nickName;
        this.password = password;
    }
}
