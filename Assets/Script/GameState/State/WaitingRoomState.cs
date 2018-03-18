using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomState : BaseState {

    public GameObject userItem;
    public Button buttonReady;
    public Button buttonCancel;
    public Text title;
    public int MAX_USER_COUNT = 3;
    List<GameObject> listUserObj = new List<GameObject>();
    public GameObject content;
    ResponseBase res = null;

    public override void initState(ResponseBase res)
    {
        this.res = res; 
        this.gameObject.SetActive(true);
        Debug.Log("initState res : " + res);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
        this.res = res;
        setData(res);
    }

    // Use this for initialization
    void Start () {
        for (int i = 0; i < MAX_USER_COUNT; i++)
        {
            GameObject item = Instantiate(userItem) as GameObject;
            //RoomUser itemSource = item.GetComponent<RoomUser>();
            //itemSource.delegateClick += onClick;
            item.SetActive(false);
            listUserObj.Add(item);
            item.transform.parent = content.transform;
        }
        Debug.Log("start --------- ");
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
        if(res.identifier == Common.IDENTIFIER_CREATE_ROOM)
        {
            WaitingRoomItem soucre = userItem.GetComponent<WaitingRoomItem>();
            RoomUser master = new RoomUser();
            ResponseCreateRoom resCr = (ResponseCreateRoom)res; 
            master.setData(UserInfo.Instance().email, UserInfo.Instance().nickName, true, true, resCr.total, resCr.win, resCr.lose, resCr.outCount);
        }
        else
        {
            List<RoomUser> list = ((ResponseConnectionRoom)res).userList;

            for (int i = 0; i < listUserObj.Count; i++)
            {
                if (i < list.Count)
                {
                    listUserObj[i].SetActive(true);
                    WaitingRoomItem soucre = listUserObj[i].GetComponent<WaitingRoomItem>();
                    soucre.setData(list[i]);
                }
                else
                {
                    listUserObj[i].SetActive(false);
                }
            }
        }
        
        
    }

    public void outUser()
    {

    }

}
