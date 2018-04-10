using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLogin : RequestBase {
    public string password;
    public bool isAutoLogin;

	public RequestLogin(string email, string password, bool isAutoLogin) : base(Common.IDENTIFIER_LOGIN)
    {
        base.email = email;
        this.password = password;
        this.isAutoLogin = isAutoLogin;
    }
}
