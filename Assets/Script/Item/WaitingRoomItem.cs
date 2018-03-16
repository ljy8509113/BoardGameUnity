using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomItem : MonoBehaviour {

    public Text textNickName;
    public Text textState;
    public Button buttonOut;
    public Plane planBackground;
    
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
        textNickName.text = user.nickName;
        textState.text = user.
        
    }

    public void setOutButton(bool isShow)
    {
        buttonOut.gameObject.SetActive(isShow);
    }
    
}
