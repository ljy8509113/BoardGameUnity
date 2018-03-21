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
    public GameObject content;
    
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
        List<UserInfo> resList;
        string masterEmail;

        if (res.identifier == Common.IDENTIFIER_CREATE_ROOM)
        {
            ResponseCreateRoom resCr = (ResponseCreateRoom)res;
            title.text = resCr.title;
            resList = ((ResponseCreateRoom)res).userList;
        }
        else
        {
            ResponseConnectionRoom resCr = (ResponseConnectionRoom)res;
            title.text = resCr.title;
            resList = ((ResponseConnectionRoom)res).userList;
        }
        
        for (int i = 0; i < listUserObj.Count; i++)
        {
            if (i < resList.Count)
            {
                listUserObj[i].SetActive(true);
                WaitingRoomItem soucre = listUserObj[i].GetComponent<WaitingRoomItem>();
                soucre.setData(resList[i]);

                if (resList[i].isMaster)
                    masterEmail = resList[i].email;
            }
            else
            {
                listUserObj[i].SetActive(false);
            }
        }

    }
    
    public void outUser()
    {

    }

}
