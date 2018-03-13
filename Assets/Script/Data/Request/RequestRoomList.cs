public class RequestRoomList : RequestBase {

    public int current;
    public int count;

    public RequestRoomList(int current, int count) : base(Common.IDENTIFIER_GAME_ROOM_LIST)
    {
        this.current = current;
        this.count = count;
    }
}
