using System;
using System.Collections.Generic;

[Serializable]
public class UserGameData {
    public int no;
    public string email;
    public string nickName;
    public List<NumberCard> cards;
    public bool isLose;
}
