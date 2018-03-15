using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour {
    public abstract void showState(ResponseBase res);
    public abstract void hideState();
}
