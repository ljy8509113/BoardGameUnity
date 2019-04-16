using System;
using System.Collections.Generic;

[Serializable]
public class UserGameData {
    public int no;
    public string email;
    public string nickName;
    public List<Card> cards;
    public bool isLose;
    public bool isInit;
    public int userType;

    public void setData(UserGameData data)
    {
        no = data.no;
        email = data.email;
        nickName = data.nickName;
        cards = data.cards;
        isLose = data.isLose;
        isInit = data.isInit;
        userType = data.userType;
    }
}
