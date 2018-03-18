using System;

[Serializable]
public class RoomUser {
    public string email;
    public string nickName;
    public bool isMaster;
    public bool onReady;
    public int totalCount;
    public int win;
    public int lose;
    public int outCount;

    public void setData(string email, string nickName, bool isMaster, bool onReady, int totalCount, int win, int lose, int outCount)
    {
        this.email = email;
        this.nickName = nickName;
        this.isMaster = isMaster;
        this.onReady = onReady;
        this.totalCount = totalCount;
        this.win = win;
        this.lose = lose;
        this.outCount = outCount;
    }

    public string winningRate()
    {
        float percent = ((float)(win - outCount) / (float)(totalCount - outCount)) * 100;        
        return string.Format("{0:#.##}", percent);
    }
}
