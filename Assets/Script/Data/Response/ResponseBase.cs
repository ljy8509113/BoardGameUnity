using System.Collections;
using System.Collections.Generic;

public class ResponseBase
{
    public string identifier;
    public string resCode;
    public string message;

    public ResponseBase()
    {

    }

    public ResponseBase(string identifier, string resCode)
    {
        this.identifier = identifier;
        this.resCode = resCode;
    }

    public ResponseBase(string identifier, string resCode, string message)
    {
        this.identifier = identifier;
        this.resCode = resCode;
        this.message = message;
    }
}
