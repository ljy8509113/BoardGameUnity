using UnityEngine;
using UnityEngine.UI;

public class CreateRoomState : BaseState {

    public InputField titleFile;
    public Dropdown dropDownUserCount;
    
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
        GameManager.Instance().stateChange(GameManager.GAME_STATE.WAITING_ROOM, res);
    }

    // Use this for initialization
    void Start () {        
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public void makeRoom()
    {
        Debug.Log("title : " + titleFile.text);
        Debug.Log("user : " + dropDownUserCount.options[dropDownUserCount.value].text);

        string title = titleFile.text;
        int maxUserCount = int.Parse(dropDownUserCount.options[dropDownUserCount.value].text);

        RequestCreateRoom cr = new RequestCreateRoom(maxUserCount, title, UserInfo.Instance().nickName);
        SocketManager.Instance().sendMessage(cr);

    }

    public void sendMessage()
    {
        RequestTest test = new RequestTest();
        SocketManager.Instance().sendMessage(test);
    }

}
