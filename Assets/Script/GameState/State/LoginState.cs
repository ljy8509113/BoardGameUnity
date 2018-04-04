using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginState : BaseState
{
    public InputField fieldEmail;
    public InputField fieldPassword;
    public Toggle toggleAuto;

   public override void initState(ResponseBase res)
    {
        this.gameObject.SetActive(true);

        if(res == null)
        {
            toggleAuto.isOn = false;
        }
        else
        {
            loginResult((ResponseLogin)res);
        }

    }

    public override void hideState()
    {
        this.gameObject.SetActive(false);
    }

    public override void updateState(ResponseBase res)
    {
        loginResult((ResponseLogin)res);
    }

    // Use this for initialization
    void Start()
    {
        fieldEmail.text = "test1@gmail.com";
        fieldPassword.text = "1234";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onLogin()
    {
        string email = fieldEmail.text;
        string password = fieldPassword.text;

        if (Common.isMailCheck(email))
        {
            string passwordCryp = Security.Instance().cryption(password, false);
            Debug.Log("check email : true // password : " + passwordCryp);
            Debug.Log("password : " + Security.Instance().deCryption(passwordCryp, false));

            RequestLogin login = new RequestLogin(email, passwordCryp, toggleAuto.isOn);
            SocketManager.Instance().sendMessage(login);
        }
        else
        {
            Debug.Log("check email : " + false);
        }        
    }

    public void onJoin()
    {
        //GameController.Instance().changeState(GameController.OBJECT_INDEX.JOIN);
        GameManager.Instance().stateChange(GameManager.GAME_STATE.JOIN, null);
    }

    public void onChangeValue()
    {
        Debug.Log("valueChange : " + toggleAuto.isOn);        
    }
    
    void loginResult(ResponseLogin res)
    {
        UserManager.Instance().setData(res.email, res.nickName);
        Debug.Log("email : " + UserManager.Instance().email);
        Debug.Log("nickName : " + UserManager.Instance().nickName);

        if (res.isAutoLogin)
        {
            Debug.Log("login : isAuto");
            try
            {
                string pw = Security.Instance().deCryption(res.password, false);
                Debug.Log("login pw : " + pw);
                
                PlayerPrefs.SetString(Common.KEY_EMAIL, res.email);
                PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 1);
                PlayerPrefs.SetString(Common.KEY_PASSWORD, Security.Instance().cryption(res.password, true));

                RequestGamingUser gaming = new RequestGamingUser();
                SocketManager.Instance().sendMessage(gaming);
            }
            catch (Exception e)
            {
                Debug.Log("login error : " + e.Message);
            }
        }
        else
        {
            PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 0);
            PlayerPrefs.SetString(Common.KEY_PASSWORD, "");
            Debug.Log("login : isAuto f");

            RequestGamingUser gaming = new RequestGamingUser();
            SocketManager.Instance().sendMessage(gaming);

        }

        
    }

}

