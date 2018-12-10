using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
    protected BaseState(){
        SocketManager.Instance().resDelegate = responseString;
    }

    public abstract void initState(ResponseBase res);
    public abstract void hideState();
    public abstract void updateState(ResponseBase res);

    public abstract void responseString(string identifier, string json);
}
