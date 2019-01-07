using UnityEngine;
using System.Collections.Generic;

public class InitState : BaseState {

    public override void initState(ResponseBase res)
    {
        base.initState(res);
        this.gameObject.SetActive(true);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }
    
    // bool isConnection = false;
    bool connectionRec = false;
    GAME_STATE moveState;

    public override void Awake()
    {
        base.Awake();
        UserManager.Instance().loadData();
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        SocketManager.Instance().connection(connection);
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        if (connectionRec)
        {
            connectionRec = false;
            StateManager.Instance().changeState(moveState, null);
        }
	}

    public void connection(bool isConnection)
    {
        // this.isConnection = isConnection;
        if(isConnection){
            if(UserManager.Instance().email != null && UserManager.Instance().email.Equals("") == false)
            {
                string sendPw = Security.Instance().cryption(UserManager.Instance().password, false);
                RequestLogin login = new RequestLogin(UserManager.Instance().email, sendPw);
                SocketManager.Instance().sendMessage(login);
            }
            else
            {
                moveState = GAME_STATE.LOGIN;
                connectionRec = true;
            }
            //if (UserManager.Instance().isAutoLogin){
            //    string password = PlayerPrefs.GetString(Common.KEY_PASSWORD);
            //    string decPw = Security.Instance().deCryption(password, true);
            //    string sendPw = Security.Instance().cryption(decPw, false);
            //    RequestLogin login = new RequestLogin(UserManager.Instance().email, sendPw);
            //    SocketManager.Instance().sendMessage(login);
            //}else{
            //    moveState = GAME_STATE.LOGIN;
            //    connectionRec = true;   
            //}
        }else{
            Debug.Log("socket fail");
            showAlert("socket_connection_error", "서버 연결에 싪패하였습니다. 잠시후 이용해 주세요.", false, false, (AlertData data, bool result, string fieldText)=>{

            });
        }
    }
    public override void responseString(bool isSuccess, string identifier, string json){
        if(isSuccess){
            moveState = GAME_STATE.ROOM_LIST;
        }else{
            moveState = GAME_STATE.LOGIN;
        }
        connectionRec = true;
    }

}
