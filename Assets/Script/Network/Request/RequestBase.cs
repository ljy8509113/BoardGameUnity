using UnityEngine;

[SerializeField]
public class RequestBase {
    
    public string email;
    public int gameNo;
    public string identifier;
    public int roomNo;
    public RequestBase(string identifier)
    {
        setData(identifier, -1, -1, UserManager.Instance().email);
    }    

    public RequestBase(string identifier, int gameNo, int roomNo){
        setData(identifier, gameNo, roomNo, UserManager.Instance().email);
    }

    public RequestBase(string identifier, int roomNo){
        setData(identifier, -1, roomNo, UserManager.Instance().email);
    }

    public RequestBase(string identifier, int gameNo, int roomNo, string email)
    {
        setData(identifier, gameNo, roomNo, email);
    }

    void setData(string identifier, int gameNo, int roomNo, string email)
    {
        this.identifier = identifier;
        this.gameNo = gameNo;
        this.roomNo = roomNo;
        this.email = email;
    }
}
