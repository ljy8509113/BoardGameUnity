using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomState : BaseState {

    public GameObject userItem;
    public Button buttonReady;
    public Button buttonCancel;
    public Text title;
    public int MAX_USER_COUNT = 4;
    List<GameObject> listUserObj = new List<GameObject>();
    List<UserInfo> listUsers = new List<UserInfo>();
    public GameObject content;
    UserInfo myInfo;

    bool isMaster = false;
    int roomNo;
    
    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
        Debug.Log("initState res : " + res);
        setData(res);
        setButton();
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
        setData(res);
        setButton();
    }

    void Awake()
    {
        for (int i = 0; i < MAX_USER_COUNT; i++)
        {
            GameObject item = Instantiate(userItem) as GameObject;
            //item.SetActive(false);
            listUserObj.Add(item);
            item.transform.parent = content.transform;
        }
        Debug.Log("awake --------- ");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start --------- ");
        setButton();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onReady()
    {
        Debug.Log("onReady -- ");
        if (isMaster)
        {
            if (isAllReady())
            {
                RequestStart req = new RequestStart(roomNo);
                SocketManager.Instance().sendMessage(req);
            }
            else
            {
				GameController.Instance().showAlert("모두 준비상태가 되어야 시작가능합니다.", false, null,false);
            }            
        }
        else
        {
            bool isReady = true;
            if (myInfo.state == (int)Common.USER_STATE.READY)
            {
                isReady = false;
            }
            
            RequestReady req = new RequestReady(isReady, roomNo);
            SocketManager.Instance().sendMessage(req);
        }
    }

    public void onCancel()
    {
        RequestOutRoom req = new RequestOutRoom(roomNo, UserManager.Instance().email);
        SocketManager.Instance().sendMessage(req);
    }

    void setData(ResponseBase res)
    {
        switch (res.identifier)
        {
            case Common.IDENTIFIER_CREATE_ROOM :
                {
                    ResponseCreateRoom resCr = (ResponseCreateRoom)res;
                    title.text = resCr.title;
                    setUsersData(((ResponseCreateRoom)res).userList);
                    isMaster = true;
                    roomNo = resCr.roomNo;
                }
                break;
            case Common.IDENTIFIER_CONNECT_ROOM:
                {
                    ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
                    title.text = resCr.title;
                    setUsersData(((ResponseConnectionRoom)res).userList);
                    roomNo = resCr.roomNo;
                }
                break;
            case Common.IDENTIFIER_READY:
                {
                    ResponseReady resReady = (ResponseReady)res;
                    for(int i=0; i<listUsers.Count; i++)
                    {
                        if (resReady.email.Equals(listUsers[i].email))
                        {
							listUsers[i].state = resReady.isReady == true ? (int)Common.USER_STATE.READY : (int)Common.USER_STATE.NONE;
                        }
                    }
                    setUsersData(listUsers);
                }
                break;
            case Common.IDENTIFIER_OUT_ROOM:
                {

                    GameManager.Instance().stateChange(GameManager.GAME_STATE.ROOM_LIST, null);
                }
                break;
            case Common.IDENTIFIER_ROOM_USERS:
                {
                    ResponseRoomUsers resUsers = (ResponseRoomUsers)res;
                    setUsersData(resUsers.userList);
                }
                break;
        }
    }
    
    void setUsersData(List<UserInfo> users)
    {
        listUsers = users;
        for (int i = 0; i < listUserObj.Count; i++)
        {
            if (i < listUsers.Count)
            {
                listUserObj[i].SetActive(true);
                WaitingRoomItem source = listUserObj[i].GetComponent<WaitingRoomItem>();

                if (listUsers[i].email == UserManager.Instance().email)
                {
                    myInfo = listUsers[i];
                    source.setData(listUsers[i], false);
                }
                else
                {
                    source.setData(listUsers[i], isMaster);
                }
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
        if(listUsers.Count > 1)
        {
            for(int i=0; i<listUsers.Count; i++)
            {
                UserInfo info = listUsers[i];

                if (listUsers[i].state != (int)Common.USER_STATE.READY)
                {
                    isAllReady = false;
                }
            }
        }
        else
        {
            buttonReady.enabled = false;
        }

        return isAllReady;        
    }

    public void outUser(string email)
    {
        RequestOutRoom req = new RequestOutRoom(roomNo, email);
        SocketManager.Instance().sendMessage(req);
    }

}
