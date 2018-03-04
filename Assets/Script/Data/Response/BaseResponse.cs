using System.Collections;
using System.Collections.Generic;

public class BaseResponse {
    public string identifier;
    public string resCode;
    public string message;

    public BaseResponse()
    {

    }

    public BaseResponse(string identifier, string resCode)
    {
        this.identifier = identifier;
        this.resCode = resCode;
    }

    public BaseResponse(string identifier, string resCode, string message)
    {
        this.identifier = identifier;
        this.resCode = resCode;
        this.message = message;
    }
}
