public class ResponseBase
{
    public string identifier;
    public int resCode;
    public string message;

    public ResponseBase()
    {

    }

    public ResponseBase(string identifier, int resCode)
    {
        this.identifier = identifier;
        this.resCode = resCode;
    }

    public ResponseBase(string identifier, int resCode, string message)
    {
        this.identifier = identifier;
        this.resCode = resCode;
        this.message = message;
    }

    public bool isSuccess()
    {
        if (resCode == 0)
            return true;
        else
            return false;
    }
}
