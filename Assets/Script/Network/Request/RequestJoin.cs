using System;

public class RequestJoin : RequestBase {
    public string password;
    public string nickName;

    public RequestJoin(string email, string nickName, string password) : base(Common.IDENTIFIER_JOIN)
    {
        base.email = email;
        this.password = password;
        this.nickName = nickName;
    }
	
}
