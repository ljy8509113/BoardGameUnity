using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomItem : MonoBehaviour {

    public Text textNickName;
    public Text textState;
    public Button buttonOut;
    public Plane planBackground;
    public Text textMaster;
    private UserInfo userInfo;

    public delegate void OutUser(string email);
    public OutUser outReceiver;

	// Use this for initialization
	void Start () {
        buttonOut.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onOut()
    {
        if(outReceiver != null)
            outReceiver(userInfo.email);
    }

    public void setData(UserInfo user)
    {
        userInfo = user;
        textNickName.text = user.nickName;

        if (user.isMaster)
            textState.text = "방장";
        else
            textState.text = user.state == (int)Common.USER_STATE.READY ? "Ready" : ""; //true ? "Ready" : "";        
    }

    public void setOutButton(bool isShow)
    {
        buttonOut.gameObject.SetActive(isShow);
    }

    public UserInfo getUserData()
    {
        return userInfo;
    }
    
}
