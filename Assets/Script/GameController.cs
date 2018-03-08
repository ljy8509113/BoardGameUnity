using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject socketManagerObj;
    SocketManager socketManager;

    public GameObject listView;
    ListView listViewCode;
    bool isActivity = false;
    bool isChangeObj = false;

    public GameObject createRoom;
    
    void Awake()
    {
        socketManager = socketManagerObj.GetComponent<SocketManager>();
        listViewCode = listView.GetComponent<ListView>();
        socketManager.resDelegate += responseString;
        createRoom.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isChangeObj)
        {
            listView.SetActive(isActivity);
            isChangeObj = false;
        }        
	}

    public void responseString(string identifier, string json)
    {
        switch (identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST:
                {
                    ResponseRoomList roomList = JsonUtility.FromJson<ResponseRoomList>(json);
                    listViewCode.setData(roomList);
                    isActivity = true;
                    isChangeObj = true;
                    //listView.SetActive(true);
                }
                break;
            case Common.IDENTIFIER_GAMING_USER:
            {
                ResponseGamingUser res = JsonUtility.FromJson<ResponseGamingUser>(json);
                Debug.Log(res.isGaming + " / " +res.textMsg);
                
                if(res.isGaming == false)
                    {
                        RequestRoomList list = new RequestRoomList(0, Common.LIST_COUNT);
                        sendMessage(list);
                    }
                    else
                    {

                    }
            }
            break;

            case Common.IDENTIFIER_CREATE_ROOM:
                {
                    ResponseCreateRoom res = JsonUtility.FromJson<ResponseCreateRoom>(json);
                    
                }
                break;

            case Common.IDENTIFIER_TEST:
                {
                    ResponseTest res = JsonUtility.FromJson<ResponseTest>(json);
                    Debug.Log("test res : " + res.message);
                }

                break;

        }
        
    }

    public void sendMessage(object str)
    {
        socketManager.sendMessage(str);
    }

    public void onCreateRoom()
    {
        createRoom.SetActive(true);
    }
    
}
