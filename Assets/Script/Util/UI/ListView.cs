using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListView : MonoBehaviour
{
    public GameObject background;
    public GameObject roomItem;

    void Start()
    {
       
    }

    void Update()
    {
        
    }

    public void setItem(ResponseBase res)
    {
        switch (res.identifier)
        {
            case Common.IDENTIFIER_GAME_ROOM_LIST :
                ResponseRoomList resRoomList = (ResponseRoomList)res;

                if(resRoomList.list != null && resRoomList.list.Count > 0)
                {
                    foreach(ResponseRoomList.Room room in resRoomList.list)
                    {

                    }
                }
                else
                {
                    //목록없음
                }
                
                break;
        }
    }
}

