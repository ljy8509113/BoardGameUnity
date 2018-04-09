public class RequestOutRoom : RequestBase {

    public int roomNo;
    public string outUser;
    public RequestOutRoom(int roomNo, string outUser) : base(Common.IDENTIFIER_OUT_ROOM){
        this.roomNo = roomNo;
        this.outUser = outUser;
    }
}
