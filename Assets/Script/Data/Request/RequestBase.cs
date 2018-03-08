using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RequestBase {
    
    public string uuid;
    public int gameNo;
    public string identifier;
    
    public void setIdentifier(string identifier)
    {
        this.identifier = identifier;
        uuid = Common.getUUID();
        gameNo = Common.GAME_NO;
    }
    
}
