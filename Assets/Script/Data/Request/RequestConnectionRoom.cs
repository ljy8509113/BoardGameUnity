using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestConnectionRoom : RequestBase {

    int roomId;

    public RequestConnectionRoom(int roomId)
    {
        base.setIdentifier(Common.IDENTIFIER_CONNECT_ROOM);
        this.roomId = roomId;
    }
}
