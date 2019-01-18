using System;

[Serializable]
public class UserInfo {
    public string email;
    public string nickName;
    public int type;
    public int state;
	
	public void setData(string email, string nickName, int type, int state)
    {
        this.email = email;
        this.nickName = nickName;
        this.type = type;
        this.state = state;        
    }

    public bool equalsType(Common.USER_TYPE type)
    {
        if (this.type == (int)type)
            return true;
        else
            return false;
    }

    //public string winningRate()
    //{
    //    float percent = ((float)(win - outCount) / (float)(totalCount - outCount)) * 100;        
    //    return string.Format("{0:#.##}", percent);
    //}
}
