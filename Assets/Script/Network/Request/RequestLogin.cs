using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLogin : RequestBase {
    public string password;
	public RequestLogin(string email, string password) : base(Common.IDENTIFIER_LOGIN)
    {
        base.email = email;
        this.password = password;
    }
}
