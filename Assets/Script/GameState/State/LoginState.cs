using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginState : BaseState
{
    public InputField fieldEmail;
    public InputField fieldPassword;
    public Toggle toggleAuto;

   override public void showState(ResponseBase res)
    {
        this.gameObject.SetActive(true);

        if(res == null)
        {
            toggleAuto.isOn = false;
        }
        else
        {
            ResponseLogin resLogin = (ResponseLogin)res;
            if (resLogin.isAutoLogin)
            {
                Debug.Log("login : isAuto");
                try
                {
                    string pw = Security.Instance().deCryption(resLogin.password, false);
                    Debug.Log("login pw : " + pw);
                    UserInfo.Instance().setData(resLogin.email, resLogin.nickName);

                    PlayerPrefs.SetString(Common.KEY_EMAIL, resLogin.email);
                    PlayerPrefs.SetInt(Common.KEY_AUTO_LOGIN, 1);
                    PlayerPrefs.SetString( Common.KEY_PASSWORD, Security.Instance().cryption(resLogin.password, true) );                    
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
                
            }

            RequestGamingUser gaming = new RequestGamingUser();
            SocketManager.Instance().sendMessage(gaming);
        }

    }

    override public void hideState()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onLogin()
    {
        string email = fieldEmail.text;
        string password = fieldPassword.text;
        
        if ( Common.isMailCheck(email) )
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
    


}

