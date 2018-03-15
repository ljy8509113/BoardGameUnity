using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : BaseState {

    override public void showState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    override public void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {

        UserInfo.Instance().loadData();

        if(UserInfo.Instance().isAutoLogin)
        {
            string password = Security.Instance().deCryption(UserInfo.Instance().password, true);                
            RequestLogin req = new RequestLogin(UserInfo.Instance().email, Security.Instance().cryption(password, false), true);
            SocketManager.Instance().sendMessage(req);
        }
        else
        {
            GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
