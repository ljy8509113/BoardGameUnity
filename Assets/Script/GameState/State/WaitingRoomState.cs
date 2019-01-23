using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoomState : BaseState {

    public GameObject userItem;
    public Button buttonReady;
    public Button buttonCancel;
    public Text title;
    public Button buttonAddAI;
    int maxUser;
    List<GameObject> listUserObj = new List<GameObject>();
    List<UserInfo> listUsers = new List<UserInfo>();
    public GameObject content;
    UserInfo myInfo;

    int roomNo;
    int gameNo;

    bool isUpdate = false;
    string gameScene = null;

    public override void initState(ResponseBase res)
    {
        base.initState(res);
        this.gameObject.SetActive(true);
        Debug.Log("initState res : " + res);
        
        if(res is ResponseCreateRoom){
            ResponseCreateRoom resCr = (ResponseCreateRoom)res;
            title.text = resCr.title;
            // setUsersData(resCr.userList);
            roomNo = resCr.roomNo;
            maxUser = resCr.max;
            listUsers = resCr.userList;
            gameNo = resCr.gameNo;
        }else{
            ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
            title.text = resCr.title;
            // setUsersData(resCr.userList);
            roomNo = resCr.roomNo;
            maxUser = resCr.max;
            listUsers = resCr.userList;
            gameNo = resCr.gameNo;
        }

        foreach (GameObject obj in listUserObj)
        {
            Destroy(obj.gameObject);
        }

        listUserObj = new List<GameObject>();

        for (int i = 0; i < maxUser; i++)
        {
            GameObject item = Instantiate(userItem) as GameObject;
            //item.SetActive(false);
            listUserObj.Add(item);
            item.transform.parent = content.transform;
        }

        setUsersData();
        setButton();
    }

    public override void hideState()
    {
        foreach (GameObject obj in listUserObj)
        {
            Destroy(obj.gameObject);
        }
        
        this.gameObject.SetActive(false);
    }

    // public override void updateState(ResponseBase res)
    // {
    //     Debug.Log("updateState res : " + res);
    //     setData(res);
    //     setButton();
    // }

    public override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
    }

    // Update is called once per frame
    public override void Update () {
		base.Update();

        if(isUpdate){
            isUpdate = false;
            if(gameScene != null)
            {
                SceneManager.LoadScene(gameScene);
                gameScene = null;
            }
            else
            {
                setUsersData();
            }            
        }
	}

    public void onReady()
    {
        Debug.Log("onReady -- ");
        if (myInfo.equalsType(Common.USER_TYPE.MASTER))
        {
            if (isAllReady())
            {
                RequestStart req = new RequestStart(gameNo, roomNo);
                SocketManager.Instance().sendMessage(req);
            }
            else
            {
                // GameManager.Instance().showAlert("모두 준비상태가 되어야 시작가능합니다.", false, null,false);
                showAlert("errorStart", "모두 준비상태가 되어야 시작가능합니다.", false, false, (AlertData data, bool isOn, string fieldText) => {
                } );
            }            
        }
        else
        {
            bool isReady = true;
            if (myInfo.state == (int)Common.USER_STATE.READY)
            {
                isReady = false;
            }
            
            RequestReady req = new RequestReady(isReady, gameNo, roomNo);
            SocketManager.Instance().sendMessage(req);
        }
    }

    public void onCancel()
    {
        RequestOutRoom req = new RequestOutRoom(gameNo, roomNo, UserManager.Instance().email);
        SocketManager.Instance().sendMessage(req);
    }

    // void setData(ResponseBase res)
    // {
    //     switch (res.identifier)
    //     {
    //         case Common.IDENTIFIER_CREATE_ROOM :
    //             {
    //                 ResponseCreateRoom resCr = (ResponseCreateRoom)res;
    //                 title.text = resCr.title;
    //                 setUsersData(((ResponseCreateRoom)res).userList);
    //                 roomNo = resCr.roomNo;
    //                 UserManager.Instance().connectedRoom(roomNo);
    //             }
    //             break;
    //         case Common.IDENTIFIER_CONNECT_ROOM:
    //             {
    //                 ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
    //                 title.text = resCr.title;
    //                 setUsersData(((ResponseConnectionRoom)res).userList);
    //                 roomNo = resCr.roomNo;
    //                 UserManager.Instance().connectedRoom(roomNo);
    //             }
    //             break;
    //         case Common.IDENTIFIER_READY:
    //             {
    //                 ResponseReady resReady = (ResponseReady)res;
    //                 for(int i=0; i<listUsers.Count; i++)
    //                 {
    //                     if (resReady.email.Equals(listUsers[i].email))
    //                     {
	// 						listUsers[i].state = resReady.isReady == true ? (int)Common.USER_STATE.READY : (int)Common.USER_STATE.NONE;
    //                     }
    //                 }
    //                 setUsersData(listUsers);
    //             }
    //             break;
    //         case Common.IDENTIFIER_OUT_ROOM:
    //             {
    //                 UserManager.Instance().outRoom();
    //                 GameManager.Instance().stateChange(GameManager.GAME_STATE.ROOM_LIST, null);                    
    //             }
    //             break;
    //         case Common.IDENTIFIER_ROOM_USERS:
    //             {
    //                 ResponseRoomUsers resUsers = (ResponseRoomUsers)res;
    //                 setUsersData(resUsers.userList);
    //             }
    //             break;
    //     }
    // }
    
    void setUsersData()
    {
        // listUsers = users;

        for(int i=0; i<listUsers.Count; i++)
        {
            if (listUsers[i].email.Equals(UserManager.Instance().email))
            {
                myInfo = listUsers[i];
                break;
            }
        }

        for (int i = 0; i < listUserObj.Count; i++)
        {

            if (i < listUsers.Count)
            {
                listUserObj[i].SetActive(true);
                WaitingRoomItem source = listUserObj[i].GetComponent<WaitingRoomItem>();
                
				source.setData(listUsers[i], myInfo.equalsType(Common.USER_TYPE.MASTER));
                source.outReceiver += outUser;
            }
            else
            {
                listUserObj[i].SetActive(false);
            }
        }

        if (myInfo.equalsType(Common.USER_TYPE.MASTER))
        {
            buttonReady.enabled = isAllReady();
        }
        
    }

    void setButton()
    {
        if (myInfo.equalsType(Common.USER_TYPE.MASTER))
        {
            buttonReady.GetComponentInChildren<Text>().text = "시작";
            buttonReady.enabled = isAllReady();
            buttonAddAI.gameObject.SetActive(true);
        }
        else
        {
            buttonReady.GetComponentInChildren<Text>().text = "준비";
            buttonAddAI.gameObject.SetActive(false);
        }
    }

    bool isAllReady()
    {
        bool isAllReady = true;

        if (listUsers.Count >= 2)
        {
            for(int i=0; i<listUsers.Count; i++)
            {
                UserInfo info = listUsers[i];

                if ( listUsers[i].state != (int)Common.USER_STATE.READY)
                {
                    isAllReady = false;
                }
            }
        }
        else
        {
            return false;
        }

        return isAllReady;        
    }

    public void outUser(string email)
    {
        RequestOutRoom req = new RequestOutRoom(gameNo, roomNo, email);
        SocketManager.Instance().sendMessage(req);
    }

    public void addAI()
    {
        if(listUsers.Count < maxUser)
        {
            RequestConnectionRoom req = new RequestConnectionRoom(roomNo, "", true);
            SocketManager.Instance().sendMessage(req);
        }
        else
        {

        }
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        if(isSuccess){
            switch (identifier)
            {
                case Common.IDENTIFIER_CONNECT_ROOM:
                    {
                        ResponseConnectionRoom resCr = JsonUtility.FromJson<ResponseConnectionRoom>(json);
                        listUsers = resCr.userList;
                        isUpdate = true;
                        
                    }
                    break;
                case Common.IDENTIFIER_READY:
                    {
                        ResponseReady resReady = JsonUtility.FromJson<ResponseReady>(json);
                        for(int i=0; i<listUsers.Count; i++)
                        {
                            if (resReady.email.Equals(listUsers[i].email))
                            {
                                listUsers[i].state = resReady.isReady == true ? (int)Common.USER_STATE.READY : (int)Common.USER_STATE.NONE;
                            }
                        }
                        // setUsersData(listUsers);
                        isUpdate = true;
                    }
                    break;
                case Common.IDENTIFIER_OUT_ROOM:
                    {
                        // UserManager.Instance().outRoom();
                        // GameManager.Instance().stateChange(GameManager.GAME_STATE.ROOM_LIST, null);                    
                        StateManager.Instance().changeState(GAME_STATE.ROOM_LIST, null);
                    }
                    break;
                case Common.IDENTIFIER_ROOM_USERS:
                    {
                        ResponseRoomUsers resUsers = JsonUtility.FromJson<ResponseRoomUsers>(json);
                        // setUsersData(resUsers.userList);
                        listUsers = resUsers.userList;
                        isUpdate = true;
                    }
                    break;
                case Common.IDENTIFIER_START :
                    {
                        // ResponseStart res = JsonUtility.FromJson<ResponseStart>(json);
                        //UserManager.Instance().roomNo = roomNo;
                        switch(gameNo){
                            case (int)Common.GAME_KINDS.DAVINCICODE :
                            {
                                UserManager.Instance().gameInitJson = json;
                                gameScene = "davincicode";
                                isUpdate = true;
                            }
                            break;
                        }
                    }    
                    break;
                case Common.IDENTIFIER_ROOM_INFO:
                    {
                        ResponseRoomInfo res = JsonUtility.FromJson<ResponseRoomInfo>(json);
                        listUsers = res.userList;
                        isUpdate = true;
                    }
                    break;
                    
            }
            
        }else{
            ResponseBase res = JsonUtility.FromJson<ResponseBase>(json);
            showAlert("error", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
        }
        
    }
}
