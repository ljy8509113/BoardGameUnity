using UnityEngine;
using System.Collections.Generic;

public class InitState : BaseState {

    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }
    
	class test{
		public List<UserGameData> arrayUser;
		public Dictionary<int, Card> mapFieldCards;
	}
	public TextAsset json;

    // bool isConnection = false;
    bool connectionRec = false;
    GAME_STATE moveState;

    // Use this for initialization
    override void Start () {
        base.Start();
        UserManager.Instance().loadData();
        SocketManager.Instance().connection(connection);
        
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
    override void Update () {
        base.Update();
        // if (connectionRec)
        // {
        //     connectionRec = false;
        //     GameController.Instance().setDelegate();
        //     if (isConnection)
        //     {
        //         if (UserManager.Instance().isAutoLogin)
        //         {
        //             string password = Security.Instance().deCryption(UserManager.Instance().password, true);
        //             RequestLogin req = new RequestLogin(UserManager.Instance().email, Security.Instance().cryption(password, false), true);
        //             SocketManager.Instance().sendMessage(req);
        //         }
        //         else
        //         {
        //             GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
        //         }
        //     }
        //     else
        //     {
        //         GameManager.Instance().showAlert("서버와의 연결에 실패하였습니다. 잠시후 다시 이용해 주세요.", false, (bool result, string fieldText) => {

        //         }, false);
        //     }
        // }

        if(connectionRec){
            connectionRec = false;
            StateManager.Instance().changeState(moveState, null);
            // if(isConnection){
            //     if (UserManager.Instance().isAutoLogin)
            //     {
                    
            //     }else{
            //         // GameManager.Instance().stateChange(GameManager.GAME_STATE.LOGIN, null);
            //         StateManager.Instance().changeState(GAME_STATE.LOGIN, null);
            //     }
            // }else{
            //     // GameManager.Instance().showAlert("서버와의 연결에 실패하였습니다. 잠시후 다시 이용해 주세요.", false, (bool result, string fieldText) => {
            //     // }, false);
                
            // }
        }
	}

    public void connection(bool isConnection)
    {
        // this.isConnection = isConnection;
        if(isConnection){
            if (UserManager.Instance().isAutoLogin){
                string password = PlayerPrefs.GetString(Common.KEY_PASSWORD);
                string decPw = Security.Instance().deCryption(password, true);
                string sendPw = Security.Instance().cryption(decPw, false);
                RequestLogin login = new RequestLogin(UserManager.Instance().email, sendPw);
                SocketManager.Instance().sendMessage(login);
            }else{
                moveState = GAME_STATE.LOGIN;
                connectionRec = true;   
            }
        }else{
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
