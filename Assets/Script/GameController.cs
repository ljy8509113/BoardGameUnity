using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject socketManagerObj;
    SocketManager socketManager;
    void Awake()
    {
        socketManager = socketManagerObj.GetComponent<SocketManager>();
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
            case "game_room_list":
                {
                    ResponseRoomList roomList = JsonUtility.FromJson<ResponseRoomList>(json);
                    Debug.Log("result : " + roomList.list[0].no);
                    Debug.Log("result : " + roomList.list[0].title);
                }
                break;
        }
        
    }

    void sendMessage(string str)
    {
        socketManager.sendMessage(str);
    }
}
