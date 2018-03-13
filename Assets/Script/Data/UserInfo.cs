

public class UserInfo {
    private static UserInfo instance = null;

    public static UserInfo Instance()
    {
        if (instance == null)
            instance = new UserInfo();

        return instance;
    }

    public string email = "";
    public string nickName = "";

    public void setData(string email, string nickName)
    {
        this.email = email;
        this.nickName = nickName;
    }
    
}
