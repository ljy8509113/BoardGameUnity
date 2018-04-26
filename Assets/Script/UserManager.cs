
using UnityEngine;

public class UserManager
{
    private static UserManager instance = null;
    
    public static UserManager Instance()
    {
        if (instance == null)
        {
            instance = new UserManager();//GameObject.FindObjectOfType(typeof(UserManager)) as UserManager;
        }

        return instance;
    }

    public string email;
    public string nickName;
    public bool isAutoLogin;
    public string password;
    public int roomNo = Common.NO_DATA;
    public bool isMaster = false;

    public void setData(string email, string nickName)
    {
        this.email = email;
        this.nickName = nickName;
    }

    public void saveEmail(string email)
    {
        this.email = email;
        PlayerPrefs.SetString(Common.KEY_EMAIL, email);
    }

    public void savePassword(string password)
    {
        this.password = password;
        PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(password, true));
    }

    public void saveAutoLogin(bool isAuto)
    {
        isAutoLogin = isAuto;
        PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, isAuto == true ? 1:0);
    }

    public void loadData()
    {
        email = PlayerPrefs.GetString(Common.KEY_EMAIL);
        password = PlayerPrefs.GetString(Common.KEY_PASSWORD);
        isAutoLogin = PlayerPrefs.GetInt(Common.KEY_AUTO_LOGIN) == 1 ? true : false;
    }

    public void connectedRoom(int roomNo)
    {
        this.roomNo = roomNo;
    }

    public void outRoom()
    {
        roomNo = Common.NO_DATA;
        isMaster = false;
    }

    public void setMaster(bool isMaster)
    {
        this.isMaster = isMaster;
    }

	public void removeData(){
		PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 0);
		PlayerPrefs.SetString(Common.KEY_PASSWORD, "");
		PlayerPrefs.SetInt (Common.KEY_AUTO_LOGIN, 0);

		email = "";
		password = "";
		isAutoLogin = false;
	}
    
}
