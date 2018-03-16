using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomState : BaseState {

    public GameObject userItem;
    public Button buttonReady;
    public Button buttonCancel;
    public Text title;

    override public void showState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
    }

    override public void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onReady()
    {

    }

    public void onCancel()
    {

    }

    void setData(ResponseBase res)
    {
        List<RoomUser> list = ((ResponseConnectionRoom)res).userList;

        foreach(RoomUser item in list)
        {
            if (item.isMaster)
            {
                WaitingRoomItem masterUser = userItem.GetComponent<WaitingRoomItem>();
                masterUser.
            }
            else
            {

            }
        }
    }

}
