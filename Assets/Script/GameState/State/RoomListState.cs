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

    bool isUpdateList = false;
    
    public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);
        // if(res != null)
        // {
        //     updateRoomList(res);
        // }
        // else
        // {
        //     RequestRoomList req = new RequestRoomList(1, Common.LIST_COUNT);
        //     SocketManager.Instance().sendMessage(req);
        // }
    }
    
    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // public override void updateState(ResponseBase res)
    // {
    //     updateRoomList(res);
    // }

    // void updateRoomList(ResponseBase res)
    // {
    //     ResponseRoomList resRoomList = (ResponseRoomList)res;
    //     current = resRoomList.current;
    //     maxCount = resRoomList.max;

    //     if (resRoomList.list != null && resRoomList.list.Count > 0)
    //     {
    //         roomDataList = resRoomList.list;

    //         int currentCount = current * Common.LIST_COUNT;

    //         if (currentCount > maxCount)
    //             currentCount = maxCount;

    //         bottomNaviText.text = currentCount + "/" + maxCount;
    //         for (int i = 0; i < Common.LIST_COUNT; i++)
    //         {
    //             if (i < roomDataList.Count)
    //             {
    //                 listItem[i].SetActive(true);
    //                 ResponseRoomList.Room room = roomDataList[i];
    //                 listItem[i].GetComponent<RoomItem>().setData(room);
    //             }
    //             else
    //             {
    //                 listItem[i].SetActive(false);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         //목록없음
    //     }
    // }

    void Awake()
    {

    }

    override void Start()
    {
        base.Start();
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

    override void Update()
    {
       base.Update();

       if(isUpdateList){
           isUpdateList = false;
           updateRoomList();
       }
    }
    
    void updateRoomList(){
        if(roomDataList != null && roomDataList.Count > 0){
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
        }else{
            //목록없음
        }
    }
    
    public void onClick(ResponseRoomList.Room item)
    {
        //Debug.Log("" + item.getIndex());
        Debug.Log("index : " + item.title);

		if (item.password == null || item.password.Length == 0) {
			RequestConnectionRoom req = new RequestConnectionRoom(item.no, UserManager.Instance().nickName);
			SocketManager.Instance().sendMessage(req);
		} else {
            // GameManager.Instance ().showAlert ("비밀번호를 입력해주세요.", true, (bool result, string fieldText) => {
			// 	if(result){
			// 		RequestRoomPassword req = new RequestRoomPassword(item.no, fieldText);
			// 		SocketManager.Instance().sendMessage(req);
			// 	}else{
				
			// 	}
			// }, true);
            showAlert("connectPasswd", "비밀번호를 입력해주세요.", true, true, (AlertData data, bool isOn, string fieldText) => {
                if(isOn){
                    RequestRoomPassword req = new RequestRoomPassword(item.no, fieldText);
                    SocketManager.Instance().sendMessage(req);
                }        
            } );
		}
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
        // GameManager.Instance().stateChange(GameManager.GAME_STATE.CREATE_ROOM, null);
        StateManager.Instance().changeState(GAME_STATE.CREATE_ROOM, null);
    }

    public void onReflush()
    {
        RequestRoomList req = new RequestRoomList(current, Common.LIST_COUNT);
        SocketManager.Instance().sendMessage(req);
    }

    public override void responseString(bool isSuccess, string identifier, string json)
    {
        if(isSuccess){
            if(identifier.Equals(Common.IDENTIFIER_GAME_ROOM_LIST)){
                ResponseRoomList res = JsonUtility.FromJson<ResponseRoomList>(json);
                roomDataList = res.list;
                current = res.current;
                maxCount = res.max;
                isUpdateList = true;
            }else if(identifier.Equals(Common.IDENTIFIER_CONNECT_ROOM)){
                ResponseConnectionRoom res = JsonUtility.FromJson<ResponseConnectionRoom>(json);
                StateManager.Instance().changeState(GAME_STATE.WAITING_ROOM, res);
            }
        }else{
            showAlert("error", res.message, false, false, (AlertData data, bool isOn, string fieldText) => {
            } );
        }
        
    }

}

