using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestRoomList : RequestBase {

    
	public RequestRoomList()
    {
        base.setData("game_room_list", Common.GAME_NO, Common.getUUID());        
    }
}
