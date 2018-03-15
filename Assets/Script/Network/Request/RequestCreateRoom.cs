public class RequestCreateRoom : RequestBase {

    public int fullUser;
    public string title;
    public string nickName;

    public RequestCreateRoom(int fullUser, string title, string nickName) : base(Common.IDENTIFIER_CREATE_ROOM)
    {
        this.fullUser = fullUser;
        this.title = title;
        this.nickName = nickName;
    }
}
