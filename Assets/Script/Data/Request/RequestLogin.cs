using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLogin : RequestBase {

    public string email;
    public string password;
    public bool isAutoLogin;

	public RequestLogin(string email, string password, bool isAutoLogin)
    {
        base.setIdentifier(Common.IDENTIFIER_LOGIN);
        this.email = email;
        this.password = password;
        this.isAutoLogin = isAutoLogin;
    }
}
