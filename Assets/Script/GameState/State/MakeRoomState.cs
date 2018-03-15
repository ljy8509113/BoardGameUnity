using UnityEngine;
using UnityEngine.UI;

public class MakeRoomState : BaseState {

    public InputField titleFile;
    public Dropdown dropDownUserCount;

   override
   public void showState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    override
    public void hideState()
    {
        this.gameObject.SetActive(false);
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
