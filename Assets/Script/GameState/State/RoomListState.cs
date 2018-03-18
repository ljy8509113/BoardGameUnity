using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListState : BaseState
{
    public GameObject roomItem;
    public GameObject contentView;
    
    public RectTransform prefab;
    public ScrollRect scrollView;
    public RectTransform content;

    public List<GameObject> listItem;

    List<ResponseRoomList.Room> roomDataList;

    public Text bottomNaviText;

    int current = 1;
    int maxCount = 0;

    
    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);

        ResponseRoomList resRoomList = (ResponseRoomList)res;
        current = resRoomList.current;
        maxCount = resRoomList.max;

        if (resRoomList.list != null && resRoomList.list.Count > 0)
        {
            roomDataList = resRoomList.list;

            int currentCount = current * Common.LIST_COUNT;

            if (currentCount > maxCount)
                currentCount = maxCount;

            bottomNaviText.text = currentCount + "/" + maxCount;
            for (int i = 0; i < Common.LIST_COUNT; i++)
            {
                if (i < roomDataList.Count)
                {
                    listItem[i].SetActive(true);
                    ResponseRoomList.Room room = roomDataList[i];
                    listItem[i].GetComponent<RoomItem>().setData(room);
                }
                else
                {
                    listItem[i].SetActive(false);
                }

            }

        }
        else
        {
            //목록없음
        }        
    }
    
    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
    }

    void Awake()
    {

    }

    void Start()
    {
        for (int i = 0; i < Common.LIST_COUNT; i++)
        {
            GameObject item = Instantiate(roomItem) as GameObject;
            RoomItem itemSource = item.GetComponent<RoomItem>();
            itemSource.delegateClick += onClick;
            itemSource.setIndex(i);
            item.SetActive(false);
            listItem.Add(item);
            item.transform.parent = content.transform;
        }

        //httpManager = httpManagerObj.GetComponent<HttpManager>();

    }

    void Update()
    {
       
    }
    
    
    public void onClick(ResponseRoomList.Room item)
    {
        //Debug.Log("" + item.getIndex());
        Debug.Log("index : " + item.title);

        RequestConnectionRoom req = new RequestConnectionRoom(item.no, UserInfo.Instance().nickName);
        SocketManager.Instance().sendMessage(req);

    }


    public void onNext()
    {
        // string req = "http://" + Common.getIp()+":8895"+"?gameRoomList";
        //Debug.Log("req ip : " + req);
        //httpManager.sendRequest(req);
        int currentCount = current * Common.LIST_COUNT;
        if (currentCount >= maxCount)
        {
            Debug.Log("end count");
        }
        else
        {
            RequestRoomList list = new RequestRoomList(current + 1, Common.LIST_COUNT);
            SocketManager.Instance().sendMessage(list);
        }

    }

    public void onBefore()
    {
        if (current <= 1)
        {
            Debug.Log("start point");
        }
        else
        {
            RequestRoomList list = new RequestRoomList(current - 1, Common.LIST_COUNT);
            SocketManager.Instance().sendMessage(list);
        }
    }

    public void onCreateRoom()
    {
        GameManager.Instance().stateChange(GameManager.GAME_STATE.CREATE_ROOM, null);
    }
}

