using System;

public class RequestJoin : RequestBase {
    public string password;
    public string nickName;
    public DateTime birthday;

    public RequestJoin(string email, string password, string nickName, DateTime birthday) : base(Common.IDENTIFIER_JOIN)
    {
        base.email = email;
        this.password = password;
        this.nickName = nickName;
        this.birthday = birthday;
    }
	
}
