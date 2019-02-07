
using UnityEngine;

public class UserManager : MonoBehaviour
{
    enum SAVE_DATA_STATE
    {
        NONE = 0,
        ACCOUNT = 1,
        PASSWORD = 2,
        NICKNAME = 3
    }

    private static UserManager instance = null;
    public static UserManager Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType(typeof(UserManager)) as UserManager;
        }

        return instance;
    }

    public string email;
    public string nickName;
    public string password;
    //public bool isAutoLogin;
    //public int roomNo = Common.NO_DATA;
    // public int roomNo = Common.NO_DATA;
    // public bool isMaster = false;

    SAVE_DATA_STATE state = SAVE_DATA_STATE.NONE;
    
    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(state != SAVE_DATA_STATE.NONE)
        {
            switch (state)
            {
                case SAVE_DATA_STATE.ACCOUNT:
                    {
                        state = SAVE_DATA_STATE.NONE;
                        PlayerPrefs.SetString(Common.KEY_EMAIL, email);
                        PlayerPrefs.SetString(Common.KEY_NICKNAME, nickName);
                        PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(password, true));
                    }
                    break;
                case SAVE_DATA_STATE.PASSWORD:
                    {
                        state = SAVE_DATA_STATE.NONE;
                        PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(password, true));
                    }
                    break;
                case SAVE_DATA_STATE.NICKNAME:
                    {
                        state = SAVE_DATA_STATE.NONE;
                        PlayerPrefs.SetString(Common.KEY_NICKNAME, nickName);
                    }
                    break;
                
            }
        }
    }

    public void setData(string email, string password, string nickName)
    {
        this.email = email;
        this.password = password;
        this.nickName = nickName;
        state = SAVE_DATA_STATE.ACCOUNT;
    }

    //public void setData(string email, string nickName)
    //{
    //    this.email = email;
    //    this.nickName = nickName;
    //}

    //public void saveEmail(string email)
    //{
    //    this.email = email;
    //    PlayerPrefs.SetString(Common.KEY_EMAIL, email);
    //}

    //public void savePassword(string password)
    //{
    //    PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(password, true));
    //}

    //public void saveAutoLogin(bool isAuto)
    //{
     //   isAutoLogin = isAuto;
     //   PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, isAuto == true ? 1:0);
    //}

    public void loadData()
    {
        //email = PlayerPrefs.GetString(Common.KEY_EMAIL);
        //isAutoLogin = PlayerPrefs.GetInt(Common.KEY_AUTO_LOGIN) == 1 ? true : false;

        email = PlayerPrefs.GetString(Common.KEY_EMAIL);
        nickName = PlayerPrefs.GetString(Common.KEY_NICKNAME);
        string pw = PlayerPrefs.GetString(Common.KEY_PASSWORD);
        if (pw != null && pw.Equals("") == false)
        {
            password = Security.Instance().deCryption(pw, true);
        }
    }
    
	public void removeData(){
		PlayerPrefs.DeleteAll();
	}    
}
