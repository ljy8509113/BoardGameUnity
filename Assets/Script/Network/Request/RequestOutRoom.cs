public class RequestOutRoom : RequestBase {
    public string outUser;
    public RequestOutRoom(int gameNo, int roomNo, string outUser){
        base(Common.IDENTIFIER_OUT_ROOM, gameNo, roomNo);
        this.outUser = outUser;
    }
}
