using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomItem : MonoBehaviour {

    public Text textNickName;
    public Text textState;
    public Button buttonOut;
    public Plane planBackground;
    private UserInfo userInfo;

    public delegate void OutUser(string email);
    public OutUser outReceiver;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onOut()
    {
        if(outReceiver != null)
            outReceiver(userInfo.email);
    }

    public void setData(UserInfo user, bool isMaster)
    {
        userInfo = user;
        textNickName.text = user.nickName;

		if (userInfo.isMaster) {
			textState.text = "방장";	
			buttonOut.gameObject.SetActive (false);
		} else {
            Debug.Log("button state : " + isMaster);
			textState.text = user.state == (int)Common.USER_STATE.READY ? "Ready" : "";        
			buttonOut.gameObject.SetActive (isMaster);
		}
    }
    
    public UserInfo getUserData()
    {
        return userInfo;
    }
        
}
