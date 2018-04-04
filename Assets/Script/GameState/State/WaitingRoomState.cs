using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomState : BaseState {

    public GameObject userItem;
    public Button buttonReady;
    public Button buttonCancel;
    public Text title;
    public int MAX_USER_COUNT = 4;
    List<GameObject> listUserObj = new List<GameObject>();
    List<UserInfo> listUsers = new List<UserInfo>();
    public GameObject content;

    bool isMaster = false;
    
    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
        Debug.Log("initState res : " + res);
        setData(res);
    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
        setData(res);
        setButton();
    }

    void Awake()
    {
        for (int i = 0; i < MAX_USER_COUNT; i++)
        {
            GameObject item = Instantiate(userItem) as GameObject;
            //item.SetActive(false);
            listUserObj.Add(item);
            item.transform.parent = content.transform;
        }
        Debug.Log("awake --------- ");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start --------- ");
        setButton();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onReady()
    {
        Debug.Log("onReady -- ");
        if (isMaster)
        {

        }
        else
        {

        }
    }

    public void onCancel()
    {

    }

    void setData(ResponseBase res)
    {
        string masterEmail;
        
        if (res.identifier == Common.IDENTIFIER_CREATE_ROOM)
        {
            ResponseCreateRoom resCr = (ResponseCreateRoom)res;
            title.text = resCr.title;
            listUsers = ((ResponseCreateRoom)res).userList;
            isMaster = true;
        }
        else
        {
            ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
            title.text = resCr.title;
            listUsers = ((ResponseConnectionRoom)res).userList;
            isMaster = false;
            
        }
        
        for (int i = 0; i < listUserObj.Count; i++)
        {
            if (i < listUsers.Count)
            {
                listUserObj[i].SetActive(true);
                WaitingRoomItem soucre = listUserObj[i].GetComponent<WaitingRoomItem>();
                soucre.setData(listUsers[i]);

                if (listUsers[i].isMaster)
                    masterEmail = listUsers[i].email;
            }
            else
            {
                listUserObj[i].SetActive(false);
            }
        }

        
    }
    
    void setButton()
    {
        if (isMaster)
        {
            buttonReady.GetComponentInChildren<Text>().text = "시작";
            buttonReady.enabled = isAllReady();
        }
        else
        {
            buttonReady.GetComponentInChildren<Text>().text = "준비";
        }
    }

    bool isAllReady()
    {
        bool isAllReady = true;
        if(listUsers.Count > 1)
        {
            for(int i=0; i<listUsers.Count; i++)
            {
                UserInfo info = listUsers[i];

                if (listUsers[i].state != (int)Common.USER_STATE.READY)
                {
                    isAllReady = false;
                }
            }
        }
        else
        {
            buttonReady.enabled = false;
        }

        return isAllReady;        
    }

    public void outUser()
    {

    }

}
