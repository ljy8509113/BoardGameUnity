﻿public class InitState : BaseState {

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

    bool isConnection = false;
    bool connectionRec = false;

    // Use this for initialization
    void Start () {

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
    void Update () {
        if (connectionRec)
        {
            connectionRec = false;
            GameController.Instance().setDelegate();
            if (isConnection)
            {
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
            }
            else
            {
                GameManager.Instance().showAlert("서버와의 연결에 실패하였습니다. 잠시후 다시 이용해 주세요.", false, (bool result, string fieldText) => {

                }, false);
            }
        }
	}

    public void connection(bool isConnection)
    {
        this.isConnection = isConnection;
        connectionRec = true;        
    }
}
