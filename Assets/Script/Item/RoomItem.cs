using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

    public Text txtTitle;
    public Text txtUserCount;
    public Text txtMasterNickName;
    public Button button;
    private int index = 0;
    
    ResponseRoomList.Room roomInfo;

    public delegate void onClickDelegate(ResponseRoomList.Room item);
    public onClickDelegate delegateClick = null;

    // Use this for initialization
    void Start () {
        button.onClick.AddListener(onClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setData(ResponseRoomList.Room roomInfo)
    {
        this.roomInfo = roomInfo;
        txtTitle.text = this.roomInfo.title;
        txtUserCount.text = this.roomInfo.maxUser + ""; //this.roomInfo.currentUser + "/" + this.roomInfo.maxUser;
        txtMasterNickName.text = roomInfo.masterUserNickName;
    }

    public void setIndex(int index)
    {
        this.index = index;
    }

    public int getIndex()
    {
        return index;
    }

    public void onClick()
    {
        delegateClick(this.roomInfo);
    }
}
