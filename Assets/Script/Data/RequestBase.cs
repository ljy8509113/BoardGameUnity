using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class RequestBase {
    
    public string uuid;
    public int gameNo;
    public string identifier;

    public void setData(string identifier, int gameNo, string uuid)
    {
        this.uuid = uuid;
        this.identifier = identifier;
        this.gameNo = gameNo;
    }
    
}
