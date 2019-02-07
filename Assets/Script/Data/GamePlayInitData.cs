
public class GamePlayInitData {
    public int roomNo;
    public int gameNo;
    public string json;

    public void removeData()
    {
        roomNo = Common.NO_DATA;
        gameNo = Common.NO_DATA;
        json = "";
    }
}
