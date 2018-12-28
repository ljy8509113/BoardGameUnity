using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RequestBase {
    
    public string email;
    public int gameNo;
    public string identifier;
    public int roomNo;
    public RequestBase(string identifier)
    {
        this.identifier = identifier;
        this.email = UserManager.Instance().email;
        this.gameNo = -1;
        this.roomNo = -1;
    }    

    public RequestBase(string identifier, int gameNo, int roomNo){
        this.identifier = identifier;
        this.email = UserManager.Instance().email;
        this.gameNo = gameNo;
        this.roomNo = roomNo;
    }

    public RequestBase(string identifier, int roomNo){
        this.identifier = identifier;
        this.email = UserManager.Instance().email;
        this.gameNo = -1;
        this.roomNo = roomNo;
    }
}
