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
    int maxUser;
    List<GameObject> listUserObj = new List<GameObject>();
    List<UserInfo> listUsers = new List<UserInfo>();
    public GameObject content;
    UserInfo myInfo;

    bool isMaster = false;
    int roomNo;
    int gameNo;

    bool isUpdate = false;
    
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
            isMaster = true;
            gameNo = resCr.gameNo;
        }else{
            ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
            title.text = resCr.title;
            // setUsersData(resCr.userList);
            roomNo = resCr.roomNo;
            maxUser = resCr.max;
            listUsers = resCr.userList;
            isMaster = false;
            gameNo = resCr.gameNo;
        }

        foreach (GameObject obj in listUserObj)
        {
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < maxUser; i++)
        {
            GameObject item = Instantiate(userItem) as GameObject;
            //item.SetActive(false);
            listUserObj.Add(item);
            item.transform.parent = content.transform;
        }

        setButton();
        setUsersData();
    }

    public override void hideState()
    {
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
            setUsersData();
        }
	}

    public void onReady()
    {
        Debug.Log("onReady -- ");
        if (isMaster)
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
        for (int i = 0; i < listUserObj.Count; i++)
        {

            if (i < listUsers.Count)
            {
                listUserObj[i].SetActive(true);
                WaitingRoomItem source = listUserObj[i].GetComponent<WaitingRoomItem>();

                if (listUsers[i].email.Equals(UserManager.Instance().email))
                {
                    Debug.Log("my info : "+ listUsers[i].isMaster);
                    myInfo = listUsers[i];
					// isMaster = listUsers[i].isMaster;
                    // UserManager.Instance().setMaster(isMaster);
                }

				source.setData(listUsers[i], isMaster);
                source.outReceiver += outUser;
            }
            else
            {
                listUserObj[i].SetActive(false);
            }
        }
    }

    void setButton()
    {
        if (isMaster)
        {
            buttonReady.GetComponentInChildren<Text>().text = "시작";
            buttonReady.enabled = isAllReady();
        }
        else
        {
            buttonReady.GetComponentInChildren<Text>().text = "준비";
        }
    }

    bool isAllReady()
    {
        bool isAllReady = true;

        if (listUsers.Count > 1)
        {
            for(int i=0; i<listUsers.Count; i++)
            {
                UserInfo info = listUsers[i];

                if (!listUsers[i].isMaster && listUsers[i].state != (int)Common.USER_STATE.READY)
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
                        // title.text = resCr.title;
                        // setUsersData(((ResponseConnectionRoom)res).userList);
                        // roomNo = resCr.roomNo;
                        // UserManager.Instance().connectedRoom(roomNo);
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
                        UserManager.Instance().roomNo = roomNo;
                        switch(gameNo){
                            case (int)Common.GAME_KINDS.DAVINCICODE :
                            {
                                SceneManager.LoadScene("Davincicode");
                            }
                            break;
                        }
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
