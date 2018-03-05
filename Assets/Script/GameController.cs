using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject socketManagerObj;
    SocketManager socketManager;

    public GameObject listView;
    ListView listViewCode;

    void Awake()
    {
        socketManager = socketManagerObj.GetComponent<SocketManager>();
        listViewCode = listView.GetComponent<ListView>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void responseString(string identifier, string json)
    {
        switch (identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST:
                {
                    ResponseRoomList roomList = JsonUtility.FromJson<ResponseRoomList>(json);
                    Debug.Log("result : " + roomList.list[0].no);
                    Debug.Log("result : " + roomList.list[0].title);

                    listViewCode.setItem(roomList);

                    listView.SetActive(true);
                }
                break;
        }
        
    }

    void sendMessage(string str)
    {
        socketManager.sendMessage(str);
    }
}
