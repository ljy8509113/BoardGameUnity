using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : BaseState {

    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
        
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
