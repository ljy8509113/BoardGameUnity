﻿public class RequestSelectUserCard : RequestBase {
    int index;
    string selectUser;
	public RequestSelectUserCard(int index, string selectUser) : base(Common.IDENTIFIER_SELECT_USER_CARD)
    {
        this.index = index;
        this.selectUser = selectUser;
    }
}
