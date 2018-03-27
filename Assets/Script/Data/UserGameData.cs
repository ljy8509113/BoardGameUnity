using System;
using System.Collections.Generic;

[Serializable]
public class UserGameData {
    private int no;
    private string email;
    private string nickName;
    private List<NumberCard> cards;
    private bool isLose;
}
