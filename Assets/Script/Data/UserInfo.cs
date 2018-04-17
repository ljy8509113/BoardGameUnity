using System;

[Serializable]
public class UserInfo {
    public string email;
    public string nickName;
    public bool isMaster;
    public int state;
	public bool isConnection;
    
	public void setData(string email, string nickName, bool isMaster, int state, bool isConnection)
    {
        this.email = email;
        this.nickName = nickName;
        this.isMaster = isMaster;
        this.state = state;        
		this.isConnection = isConnection;
    }

    //public string winningRate()
    //{
    //    float percent = ((float)(win - outCount) / (float)(totalCount - outCount)) * 100;        
    //    return string.Format("{0:#.##}", percent);
    //}
}
