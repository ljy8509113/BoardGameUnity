using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RequestBase {
    
    public string email;
    public int gameNo;
    public string identifier;
    
    public RequestBase(string identifier)
    {
        this.identifier = identifier;
        this.email = UserInfo.Instance().email;
        this.gameNo = Common.GAME_NO;
    }    
}
