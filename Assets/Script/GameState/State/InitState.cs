using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        UserManager.Instance().loadData();

        if (UserManager.Instance().isAutoLogin)
        {
            string password = Security.Instance().deCryption(UserManager.Instance().password, true);
            RequestLogin req = new RequestLogin(UserManager.Instance().email, Security.Instance().cryption(password, false), true);
            SocketManager.Instance().sendMessage(req);
        }
        else
        {
            GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
        }

        //string password = Security.Instance().cryption("1234", false);
        //string email, string password, string nickName, DateTime birthday

        //DateTime birthDay = DateTime.ParseExact("1990-01-05", "yyyy-MM-dd", null);

        //for(int i=5; i<10; i++)
        //{
        //    RequestJoin req = new RequestJoin("imsi"+i+"@gmail.com", password, "임시계정 " + i, birthDay);
        //    SocketManager.Instance().sendMessage(req);
        //}
    }

    // Update is called once per frame
    void Update () {
		
	}
}
