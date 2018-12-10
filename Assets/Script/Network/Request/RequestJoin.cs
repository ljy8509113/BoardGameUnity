using System;

public class RequestJoin : RequestBase {
    public bool isAutoId;
    public string nickName;

    public RequestJoin(string email, string nickName, bool isAutoId) : base(Common.IDENTIFIER_JOIN)
    {
        base.email = email;
        this.isAutoId = isAutoId;
        this.nickName = nickName;
    }
	
}
