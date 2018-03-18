using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomItem : MonoBehaviour {

    public Text textNickName;
    public Text textState;
    public Button buttonOut;
    public Plane planBackground;
    private RoomUser userInfo;
    
	// Use this for initialization
	void Start () {
        buttonOut.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onOut()
    {

    }

    public void setData(RoomUser user)
    {
        userInfo = user;
        textNickName.text = user.nickName;

        if (user.isMaster)
            textState.text = "";
        else
            textState.text = user.onReady == true ? "Ready" : "";        
    }

    public void setOutButton(bool isShow)
    {
        buttonOut.gameObject.SetActive(isShow);
    }

    public RoomUser getUserData()
    {
        return userInfo;
    }
    
}
