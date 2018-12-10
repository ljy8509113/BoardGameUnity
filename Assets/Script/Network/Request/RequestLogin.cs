using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestLogin : RequestBase {
    bool isAutoId;
	public RequestLogin(string email, string password, bool isAutoId) : base(Common.IDENTIFIER_LOGIN)
    {
        base.email = email;
        this.isAutoId = isAutoId;
    }
}
