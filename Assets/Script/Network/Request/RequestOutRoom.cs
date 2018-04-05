public class RequestOutRoom : RequestBase {

    int roomNo;
    string outUser;
    public RequestOutRoom(int roomNo, string outUser) : base(Common.IDENTIFIER_OUT_ROOM){
        this.roomNo = roomNo;
        this.outUser = outUser;
    }
}
