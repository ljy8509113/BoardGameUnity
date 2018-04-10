using System;

[Serializable]
public class UserInfo {
    public string email;
    public string nickName;
    public bool isMaster;
    public int state;
    
    public void setData(string email, string nickName, bool isMaster, int state)
    {
        this.email = email;
        this.nickName = nickName;
        this.isMaster = isMaster;
        this.state = state;        
    }

    //public string winningRate()
    //{
    //    float percent = ((float)(win - outCount) / (float)(totalCount - outCount)) * 100;        
    //    return string.Format("{0:#.##}", percent);
    //}
}
