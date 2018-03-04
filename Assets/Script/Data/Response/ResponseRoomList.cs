﻿using System.Collections;
using System.Collections.Generic;
using System;

public class ResponseRoomList : BaseResponse {
    [Serializable]
    public class Room
    {
        public int no;
        public string title;
        public int gameNo;
        public int fullUser;
        public int current;
        public string state;
        public string masterUuid;
    }

    public List<Room> list;
}