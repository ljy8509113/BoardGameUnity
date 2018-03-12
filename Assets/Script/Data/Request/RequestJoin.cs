using System;

public class RequestJoin : RequestBase {

    public string email;
    public string password;
    public string nickName;
    public DateTime birthday;

    public RequestJoin(string email, string password, string nickName, DateTime birthday)
    {
        base.setIdentifier(Common.IDENTIFIER_JOIN);
        this.email = email;
        this.password = password;
        this.nickName = nickName;
        this.birthday = birthday;
    }
	
}
