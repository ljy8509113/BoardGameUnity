using System.Collections.Generic;
using System;

public class ResponseRoomList : ResponseBase
{
    [Serializable]
    public class Room
    {
        public int no;
        public string title;
        public int maxUser;
        public bool isPlaing;
		public string masterUserNickName;
		public string password;
    }

	public List<Room> list;

    public int current;
    public int max;
}
