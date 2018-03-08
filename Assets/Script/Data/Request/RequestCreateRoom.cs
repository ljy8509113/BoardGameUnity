public class RequestCreateRoom : RequestBase {

    public int fullUser;
    public string title;

	public RequestCreateRoom(int fullUser, string title)
    {
        base.setIdentifier(Common.IDENTIFIER_CREATE_ROOM);
        this.fullUser = fullUser;
        this.title = title;
    }
}
