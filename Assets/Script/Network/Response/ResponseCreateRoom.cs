using System.Collections.Generic;
public class ResponseCreateRoom : ResponseBase {
    public string title;
    public List<UserInfo> userList;
    public int roomNo;
    public int max;
    public int gameNo;
}
