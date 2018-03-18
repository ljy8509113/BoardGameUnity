using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
    public abstract void initState(ResponseBase res);
    public abstract void hideState();
    public abstract void updateState(ResponseBase res);
}
