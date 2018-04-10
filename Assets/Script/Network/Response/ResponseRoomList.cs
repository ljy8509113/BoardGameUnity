using System.Collections.Generic;
using System;

public class ResponseRoomList : ResponseBase
{
    [Serializable]
    public class Room
    {
        public int no;
        public string title;
        public int gameNo;
        public int maxUser;
        public int currentUser;
        public string state;
        public string masterEmail;
        public string masterNickName;
    }

    public List<Room> list;

    public int current;
    public int max;
}
